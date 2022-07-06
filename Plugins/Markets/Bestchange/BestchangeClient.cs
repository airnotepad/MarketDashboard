using Common.RestSharp;
using Ionic.Zip;
using MarketPlugin.Base;
using RestSharp;
using System.Text;

namespace Bestchange
{
    public class BestchangeClient
    {
        private string Url = "http://api.bestchange.ru/info.zip";
        private Method Method = Method.Get;
        private string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "BCMarket.zip");
        private string FilesPath = Path.Combine(Directory.GetCurrentDirectory(), "BCMarket");

        private string Proxy = null;

        public List<BestChangeExchanger> Exchangers = new List<BestChangeExchanger>();
        public List<BestChangeRate> Rates = new List<BestChangeRate>();
        public bool IsSuccessful = false;

        public BestchangeClient(string Proxy)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.Proxy = Proxy;
        }

        public async Task<MarketResponse> GetFilesAsync()
        {
            var client = new RestClient();
            client.SetProxy(Proxy);
            var request = new RestRequest(Url, Method);

            //var response = client.DownloadData(request);

            var response = await client.ExecuteAsync(request);

            var marketResponse = new MarketResponse();

            marketResponse.StatusCode = response.StatusCode;
            marketResponse.Content = response.Content;
            marketResponse.ErrorMessage = response.ErrorMessage;
            marketResponse.Exception = response.ErrorException;

            IsSuccessful = response.IsSuccessful;
            if (response.IsSuccessful)
            {
                await File.WriteAllBytesAsync(FilePath, response.RawBytes);

                using (ZipFile zip = ZipFile.Read(FilePath))
                {
                    foreach (ZipEntry file in zip)
                    {
                        file.Extract(FilesPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                File.Delete(FilePath);
            }

            return marketResponse;
        }

        public void GetExchangers()
        {
            var path = Path.Combine(FilesPath, "bm_exch.dat");
            if (!File.Exists(path)) throw new FileNotFoundException("File 'bm_exch.dat' not found");
            string[] Names = File.ReadAllLines(path, Encoding.GetEncoding(1251));
            foreach (string line in Names)
            {
                string data = line.Replace(".", ",");
                string[] lines = data.Split(';');
                Exchangers.Add(new BestChangeExchanger() { ex_id = Convert.ToInt32(lines[0]), ex_name = lines[1] });
            }
        }

        public void GetRates()
        {
            var path = Path.Combine(FilesPath, "bm_rates.dat");
            if (!File.Exists(path)) throw new FileNotFoundException("File 'bm_rates.dat' not found");

            string[] strs = File.ReadAllLines(path, Encoding.GetEncoding(1251));
            foreach (string line in strs)
            {
                string data = line.Replace(".", ",");
                string[] lines = data.Split(';');

                int from = Convert.ToInt32(lines[0]);
                int to = Convert.ToInt32(lines[1]);
                int ex_id = Convert.ToInt32(lines[2]);
                double rateGive = Convert.ToDouble(lines[3]);
                double rateReceive = Convert.ToDouble(lines[4]);
                double rate = rateGive / rateReceive;
                double reserve = Convert.ToDouble(lines[5]);

                BestChangeRate bestChangeLine = new BestChangeRate
                {
                    from = from,
                    to = to,
                    ex_id = ex_id,
                    ex_name = Exchangers.Find(x => x.ex_id == ex_id).ex_name,
                    rate_give = rateGive,
                    rate_receive = rateReceive,
                    rate = rate,
                    reserve = reserve
                };

                Rates.Add(bestChangeLine);
            }
        }

        public double GetBestRate(int from, int to)
        {
            string[] strs = File.ReadAllLines(FilesPath + @"bm_rates.dat", Encoding.GetEncoding(1251));

            double bestRate = 0;

            foreach (string line in strs)
            {
                string data = line.Replace(".", ",");
                string[] lines = data.Split(';');

                int _from = Convert.ToInt32(lines[0]);
                int _to = Convert.ToInt32(lines[1]);
                double _rateGive = Convert.ToDouble(lines[3]);
                double _rateReceive = Convert.ToDouble(lines[4]);
                double _rate = Convert.ToDouble(Math.Round(_rateReceive / _rateGive, 4));

                if (_from == from && _to == to)
                    if (_rate > bestRate)
                        bestRate = _rate;
            }

            return bestRate;
        }

        public void RemoveFiles()
        {
            DirectoryInfo di = new DirectoryInfo(FilesPath);

            foreach (FileInfo file in di.GetFiles())
                file.Delete();

            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);

            Directory.Delete(FilesPath, true);
        }
    }
}
