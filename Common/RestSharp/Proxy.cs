using RestSharp;
using System.Net;

namespace Common.RestSharp
{
    public static class Proxy
    {
        /// <summary>
        /// Устанавливает прокси в RestClient
        /// </summary>
        /// <param name="ConnectionString">IP:PORT | IP:PORT:LOGIN:PASSWORD</param>
        public static void SetProxy(this RestClient Client, string ConnectionString)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
                Client.Options.Proxy = GetWebProxy(ConnectionString);
        }

        /// <summary>
        /// Преобразует строку подключения к прокси в объект, реализующий интерфейс IWebProxy
        /// </summary>
        /// <param name="ConnectionString">IP:PORT | IP:PORT:LOGIN:PASSWORD</param>
        /// <returns>Возвращает объект, реализующий интерфейс IWebProxy</returns>
        public static IWebProxy GetWebProxy(string ConnectionString)
        {
            string host = ConnectionString.Split(':')[0];
            string port = ConnectionString.Split(':')[1];

            IWebProxy proxy = new WebProxy($"{host}:{port}");
            if (ConnectionString.Split(':').Length > 2)
            {
                string username = ConnectionString.Split(':')[2];
                string password = ConnectionString.Split(':')[3];

                proxy.Credentials = new NetworkCredential(username, password);
            }

            return proxy;
        }
    }
}
