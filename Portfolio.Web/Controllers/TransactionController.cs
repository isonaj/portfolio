using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Model.Repositories;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers
{
    [Route("api/Transactions")]
    public class TransactionController : Controller
    {
        IRepository<Model.Portfolio> _repo;
        public TransactionController(IRepository<Model.Portfolio> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("{portfolioId}")]
        public IActionResult Index(Guid portfolioId)
        {
            var p = _repo.Get(portfolioId);
            var txns = p.Transactions;
            return View(txns);
        }

        [HttpPost]
        [Route("{portfolioId}/import")]
        public async Task<IActionResult> ImportFiles(Guid portfolioId, List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        var importer = new TransactionImporter();
                        var transactions = importer.LoadTransactions(stream);

                        var portfolio = _repo.Get(portfolioId);
                        portfolio.AddTransactions(transactions);
                        _repo.Save(portfolio);
                    }
                }
            }
            return RedirectToAction("Index", new { portfolioId });
        }
    }
}