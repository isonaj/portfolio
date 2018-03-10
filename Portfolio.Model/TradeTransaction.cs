using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model
{
    public enum TradeTypes
    {
        Buy,
        Sell
    }

    public class TradeTransaction : Transaction
    {
        public TradeTypes Type { get; private set; }
        public int Units { get; private set; }
        public decimal Fees { get; private set; }
        public decimal Total { get; private set; }

        // Calculated
        public decimal Price { get { return 0M; } }
        public decimal Amount { get; }

        public TradeTransaction(DateTime date, string code, TradeTypes type, int units, decimal fees, decimal total) : base(date, code)
        {
            Type = type;
            Units = units;
            Fees = fees;
            Total = total;
        }
    }

}
