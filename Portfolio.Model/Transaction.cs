using System;

namespace Portfolio.Model
{
    public enum TransactionTypes
    {
        Buy,
        Sell
    }

    public class Transaction
    {
        public long Id { get; private set; }
        public Guid PortfolioId { get; private set; }
        public DateTime Date { get; private set; }
        public string Code { get; private set; }
        //public string Units { get; }
        //public string Amount { get; }

        public TransactionTypes Type { get; private set; }
        public int Units { get; private set; }
        public decimal Fees { get; private set; }
        public decimal Total { get; private set; }

        // Calculated
        public decimal Price { get { return 0M; } }
        public decimal Amount { get; }

        public Transaction() { }

        public Transaction(DateTime date, string code, TransactionTypes type, int units, decimal fees, decimal total)
        {
            Date = date;
            Code = code;
            Type = type;
            Units = units;
            Fees = fees;
            Total = total;
        }

        internal void GenerateSummary(ref PortfolioSummary summary)
        {
            switch (Type)
            {
                case TransactionTypes.Buy:
                    summary.AddHolding(Date, Units, Total);
                    break;
                case TransactionTypes.Sell:
                    summary.SellHolding(Date, Units, Total);
                    break;
            }
        }
    }
}