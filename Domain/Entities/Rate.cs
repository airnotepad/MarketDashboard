namespace Domain.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public double Price { get; set; }
        public DateTime AvailableAt { get; set; }
    }
}
