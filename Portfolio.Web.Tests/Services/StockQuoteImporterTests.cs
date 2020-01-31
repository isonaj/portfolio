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
    public class StockQuoteImporterTests
    {
        [TestMethod]
        public void CanLoadFile()
        {
            var file = @"14D,20200115,.2,.2,.195,.195,231357
14DO,20200115,.038,.038,.03,.03,30001";
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(file)))
            {
                var importer = new StockQuoteImporter();
                var quotes = importer.LoadStockQuotes(stream);

                Assert.AreEqual(2, quotes.Count());
                var quote = quotes.First();
                Assert.AreEqual("14D", quote.Code);
                Assert.AreEqual(new DateTime(2020, 1, 15), quote.Date);
                Assert.AreEqual(.2M, quote.Open);
                Assert.AreEqual(.2M, quote.High);
                Assert.AreEqual(.195M, quote.Low);
                Assert.AreEqual(.195M, quote.Close);
                Assert.AreEqual(231357, quote.Volume);
            }
        }
    }
}
