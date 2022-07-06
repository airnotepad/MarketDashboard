namespace Bestchange
{
    public class BestChangeRate
    {
        public int from { get; set; } = 0;
        public int to { get; set; } = 0;
        public int ex_id { get; set; } = 0;
        public string ex_name { get; set; } = "";
        public double rate_give { get; set; } = 0;
        public double rate_receive { get; set; } = 0;
        public double rate { get; set; } = 0;
        public double reserve { get; set; } = 0;

        public string From => from.ToString();
        public string To => to.ToString();
        public string Print() => $"Name: {ex_name} RateReceive: {Math.Round(rate_receive, 2)} Reserve: {Math.Round(reserve, 2)}";
    }
}
