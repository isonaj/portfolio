using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model
{
    public class MatchedTrade
    {
        public string Code { get; private set; }
        public int Units { get; private set; }
        public DateTime BuyDate { get; private set; }
        public decimal BuyAmount { get; private set; }
        public DateTime SellDate { get; private set; }
        public decimal SellAmount { get; private set; }

        public bool IsDiscounted {  get { return (SellDate - BuyDate).TotalDays > 365; } } 
        public decimal Profit {  get { return SellAmount - BuyAmount; } }

        public MatchedTrade(string code, int units, DateTime buyDate, decimal buyAmount, DateTime sellDate, decimal sellAmount)
        {
            Code = code;
            Units = units;
            BuyDate = buyDate;
            BuyAmount = buyAmount;
            SellDate = sellDate;
            SellAmount = sellAmount;
        }
    }
}
