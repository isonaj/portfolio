using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Portfolio.Web.Tests
{
    [TestClass]
    public class TradeImporterTests
    {
        [TestMethod]
        public void CanLoadFile()
        {
            var file = "Confirmation Number,Order Number,Trade Date,Buy/ Sell,Security,Units,Average Price ($),Brokerage (inc GST.),Net Proceeds ($),Settlement Date,Confirmation Status,\n" +
                        "80415128,N95343577,6/11/2017,B,WLE,862,1.160,10.00,1009.92,8/11/2017,Confirmed,";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(file));
        }
    }
}
