using System.Net;

namespace MarketPlugin.Base
{
    public interface IMarketResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }
        public bool HasException { get; }
        public Exception Exception { get; }
        public string ErrorMessage { get; }
    }
}
