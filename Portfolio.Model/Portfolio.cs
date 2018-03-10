﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        void GenerateSummary()
        {
            _summaries = new List<PortfolioSummary>();
            foreach (var txn in _txns.OrderBy(t => t.Date))
            {
                var summary = _summaries.Where(s => s.Code == txn.Code).SingleOrDefault();
                if (summary == null)
                {
                    summary = new PortfolioSummary { Code = txn.Code };
                    _summaries.Add(summary);
                }
                txn.GenerateSummary(ref summary);
            }
        }
    }
}
