using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model
{
    public class Portfolio : IEntity<Guid>
    {
        public string Name { get; private set; }

        private List<Transaction> _txns = new List<Transaction>();
        public IEnumerable<Transaction> Transactions { get { return _txns; } }
        private List<PortfolioSummary> _summaries = new List<PortfolioSummary>();
        public IEnumerable<PortfolioSummary> Summaries {  get { return _summaries; } }
        public Portfolio(string name)
        {
            Name = name;
        }

        public void AddTransaction(Transaction txn)
        {
            _txns.Add(txn);
        }
    }
}
