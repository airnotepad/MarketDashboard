using Newtonsoft.Json;

namespace Blockchain
{
    class BlockchainData
    {
        [JsonProperty("15m")]
        public double FifteenM { get; set; }
        [JsonProperty("last")]
        public double Last { get; set; }
        [JsonProperty("buy")]
        public double Buy { get; set; }
        [JsonProperty("sell")]
        public double Sell { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}