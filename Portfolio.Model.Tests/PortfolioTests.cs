using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Model.Tests
{
    [TestClass]
    public class PortfolioTests
    {
        [TestMethod]
        public void CanCreatePortfolio()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");

            Assert.AreEqual("Test", portfolio.Name);
        }
        
        [TestMethod]
        public void CanBuyTrade()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransaction(new Transaction(DateTime.Today, "ABC", TransactionTypes.Buy, 123, 20M, 2000M));

            Assert.AreEqual(1, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(123, summ.Units);
            Assert.AreEqual(2000M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(0M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanBuyTrades()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Buy, 50, 20M, 1000M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Buy, 50, 20M, 2000M)
            });

            Assert.AreEqual(2, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(100, summ.Units);
            Assert.AreEqual(3000M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(0M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellTrade()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransaction(new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 123, 20M, 2200M));

            Assert.AreEqual(1, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(-123, summ.Units);
            Assert.AreEqual(-2200M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(0M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellTradeReverseOrder()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction>{
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 123, 20M, 2200M),
                new Transaction(DateTime.Today.AddMonths(-10), "ABC", TransactionTypes.Buy, 123, 20M, 2000M)
            });

            Assert.AreEqual(2, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(0, summ.Units);
            Assert.AreEqual(0M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(200M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellTradePartial()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransaction(new Transaction(DateTime.Today.AddMonths(-10), "ABC", TransactionTypes.Buy, 100, 20M, 2000M));
            portfolio.AddTransaction(new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 50, 20M, 1100M));

            Assert.AreEqual(2, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(50, summ.Units);
            Assert.AreEqual(1000M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(100M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellFIFOLess()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today.AddMonths(-10), "ABC", TransactionTypes.Buy, 50, 20M, 1000M),
                new Transaction(DateTime.Today.AddMonths(-9), "ABC", TransactionTypes.Buy, 50, 20M, 2000M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 25, 20M, 600M),
            });

            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(75, summ.Units);
            Assert.AreEqual(2500M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(100M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellFIFOMore()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today.AddMonths(-10), "ABC", TransactionTypes.Buy, 50, 20M, 1000M),
                new Transaction(DateTime.Today.AddMonths(-9), "ABC", TransactionTypes.Buy, 50, 20M, 2000M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 75, 20M, 3000M),
            });

            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(25, summ.Units);
            Assert.AreEqual(1000M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(1000M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellTradeWithDiscount()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransaction(new Transaction(DateTime.Today.AddMonths(-13), "ABC", TransactionTypes.Buy, 123, 20M, 2000M));
            portfolio.AddTransaction(new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 123, 20M, 2200M));

            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(0, summ.Units);
            Assert.AreEqual(0M, summ.Cost);
            Assert.AreEqual(200M, summ.DiscountedRealisedGain);
            Assert.AreEqual(0M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellFIFOLessWithDiscount()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today.AddMonths(-14), "ABC", TransactionTypes.Buy, 50, 20M, 1000M),
                new Transaction(DateTime.Today.AddMonths(-10), "ABC", TransactionTypes.Buy, 50, 20M, 2000M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 25, 20M, 600M),
            });

            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(75, summ.Units);
            Assert.AreEqual(2500M, summ.Cost);
            Assert.AreEqual(100M, summ.DiscountedRealisedGain);
            Assert.AreEqual(0M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellFIFOMoreWithDiscount()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today.AddMonths(-14), "ABC", TransactionTypes.Buy, 50, 20M, 1000M),
                new Transaction(DateTime.Today.AddMonths(-9), "ABC", TransactionTypes.Buy, 50, 20M, 1500M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Sell, 75, 20M, 3000M),
            });

            var trades = new List<MatchedTrade> {
                new MatchedTrade("ABC", 50, DateTime.Today.AddMonths(-14), 1000M, DateTime.Today, 2000M),
                new MatchedTrade("ABC", 25, DateTime.Today.AddMonths(-9), 750M, DateTime.Today, 1000M)
            };
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(25, summ.Units);
            Assert.AreEqual(750M, summ.Cost);
            Assert.AreEqual(1000M, summ.DiscountedRealisedGain);
            Assert.AreEqual(250M, summ.RealisedGain);
        }

        [TestMethod]
        public void CanSellTradeThenBuy()
        {
            var portfolio = new Portfolio(Guid.NewGuid(), "Test");
            portfolio.AddTransactions(new List<Model.Transaction> {
                new Transaction(DateTime.Today.AddDays(-5), "ABC", TransactionTypes.Sell, 50, 20M, 600M),
                new Transaction(DateTime.Today, "ABC", TransactionTypes.Buy, 100, 20M, 1000M)
            });

            Assert.AreEqual(2, portfolio.Transactions.Count());
            Assert.AreEqual(1, portfolio.Summaries.Count());
            var summ = portfolio.Summaries.First();
            Assert.AreEqual("ABC", summ.Code);
            Assert.AreEqual(50, summ.Units);
            Assert.AreEqual(500M, summ.Cost);
            Assert.AreEqual(0M, summ.DiscountedRealisedGain);
            Assert.AreEqual(100M, summ.RealisedGain);
        }
    }
}
