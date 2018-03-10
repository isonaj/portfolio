using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Model
{
    class SummaryHolding
    {
        public DateTime Date { get; private set; }
        public int Units { get; private set; }
        public decimal Cost { get; private set; }

        public SummaryHolding(DateTime date, int units, decimal cost)
        {
            Date = date;
            Units = units;
            Cost = cost;
        }
    }
    public class PortfolioSummary
    {
        List<SummaryHolding> _holdings = new List<SummaryHolding>();
        List<MatchedTrade> _trades = new List<MatchedTrade>();

        public string Code { get; set; }
        public int Units { get { return _holdings.Sum(o => o.Units); } }
        public decimal Cost { get { return _holdings.Sum(o => o.Cost); } }

        public decimal RealisedGain { get { return _trades.Where(t => !t.IsDiscounted).Sum(t => t.Profit); } }
        public decimal DiscountedRealisedGain { get { return _trades.Where(t => t.IsDiscounted).Sum(t => t.Profit); } }

        public IEnumerable<MatchedTrade> Trades { get { return _trades; } }

        public void AddHolding(DateTime date, int units, decimal cost)
        {
            _holdings.Add(new SummaryHolding(date, units, cost));
        }

        public void SellHolding(DateTime date, int units, decimal amount)
        {
            var newHold = new List<SummaryHolding>();
            foreach (var h in _holdings)
            {
                int tmpUnits;
                decimal tmpCost;
                if (units >= h.Units)
                {
                    // Sell all of the holdings
                    tmpUnits = h.Units;
                    tmpCost = h.Cost;
                }
                else
                {
                    // Partial sell
                    tmpUnits = units;
                    tmpCost = h.Cost * units / h.Units;
                    newHold.Add(new SummaryHolding(h.Date, h.Units - units, h.Cost - tmpCost));
                }

                if (tmpUnits > 0)
                {
                    var tmpAmount = tmpUnits * amount / units;
                    _trades.Add(new MatchedTrade(Code, tmpUnits, h.Date, tmpCost, date, tmpAmount));
                    amount -= tmpAmount;
                }

                units -= tmpUnits;
            }
            _holdings = newHold;
        }
    }
}