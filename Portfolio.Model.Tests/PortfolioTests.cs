using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Model;

namespace Portfolio.Model.Tests
{
    [TestClass]
    public class PortfolioTests
    {
        [TestMethod]
        public void CanCreatePortfolio()
        {
            var portfolio = new Portfolio("Test");

            Assert.AreEqual("Test", portfolio.Name);
        }
    }
}
