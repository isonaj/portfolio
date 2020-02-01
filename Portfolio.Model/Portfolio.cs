using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Portfolio.Model
{
    public class Portfolio : Entity
    {
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; private set; }

        private List<Transaction> _txns = new List<Transaction>();
        public IEnumerable<Transaction> Transactions { get { return _txns; } }
        private List<PortfolioSummary> _summaries = new List<PortfolioSummary>();
        public IEnumerable<PortfolioSummary> Summaries {  get { return _summaries; } }

        // for EF
        public Portfolio() : base(Guid.NewGuid()) { }
        public Portfolio(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public void AddTransactions(IEnumerable<Transaction> txns)
        {
            _txns.AddRange(txns);
            GenerateSummary();
        }

        public void AddTransaction(Transaction txn)
        {
            _txns.Add(txn);
            GenerateSummary();
        }

        public void GenerateSummary()
        {
            _summaries = new List<PortfolioSummary>();
            foreach (var txn in _txns.OrderBy(t => t.Date))
            {
                var summary = _summaries
                    .Where(s => s.Code == txn.Code)
                    .SingleOrDefault();
                if (summary == null)
                {
                    summary = new PortfolioSummary { Code = txn.Code };
                    _summaries.Add(summary);
                }
                summary.ApplyTransaction(txn);
            }
        }

        public void AddStockQuote(StockQuote stockQuote)
        {
            var summary = _summaries
                .Where(s => s.Code == stockQuote.Code)
                .SingleOrDefault();

            if (summary != null)
            {
                summary.MarketValue = summary.Units * stockQuote.Close;
            }
        }
    }
}
