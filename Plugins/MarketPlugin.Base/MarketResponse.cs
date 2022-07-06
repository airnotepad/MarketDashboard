using System.Net;

namespace MarketPlugin.Base
{
    public class MarketResponse : IMarketResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public bool HasException => Exception is not null;

        public Exception Exception { get; set; }

        public string ErrorMessage { get; set; }
    }
}
