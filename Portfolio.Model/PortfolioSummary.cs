namespace Portfolio.Model
{
    public class PortfolioSummary
    {
        public string Code { get; set; }
        public int Units { get; set; }
        public decimal Cost { get; set; }

        public decimal RealisedGain { get; set; }
        public decimal DiscountedRealisedGain { get; set; }
        public decimal Dividends { get; set; }
    }
}