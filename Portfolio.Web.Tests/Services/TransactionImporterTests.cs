using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Model;
using Portfolio.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Portfolio.Web.Tests.Services
{
    [TestClass]
    public class TransactionImporterTests
    {
        [TestMethod]
        public void CanLoadFile()
        {
            var file = "Confirmation Number,Order Number,Trade Date,Buy/ Sell,Security,Units,Average Price ($),Brokerage (inc GST.),Net Proceeds ($),Settlement Date,Confirmation Status,\n" +
                        "80415128,N95343577,6/11/2017,B,WLE,862,1.160,10.00,1009.92,8/11/2017,Confirmed,";
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(file)))
            {
                var importer = new TransactionImporter();
                var transactions = importer.LoadTransactions(stream);

                Assert.AreEqual(1, transactions.Count());
                var txn = transactions.First();
                Assert.AreEqual("WLE", txn.Code);
                Assert.AreEqual(new DateTime(2017, 11, 6), txn.Date);
                Assert.AreEqual(TransactionTypes.Buy, txn.Type);
                Assert.AreEqual(862, txn.Units);
                Assert.AreEqual(1009.92M, txn.Total);
                Assert.AreEqual(10M, txn.Fees);
            }
        }
    }
}
