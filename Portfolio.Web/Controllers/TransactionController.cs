using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Data;
using Portfolio.Application.Portfolio;
using Portfolio.Application.Transaction;

namespace Portfolio.Web.Controllers
{
    [Route("api/Transactions")]
    public class TransactionController : Controller
    {
        IRepository<Model.Portfolio> _repo;
        IMediator _mediator;
        public TransactionController(IMediator mediator, IRepository<Model.Portfolio> repo)
        {
            _repo = repo;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{portfolioId}")]
        public async Task<IActionResult> Index(Guid portfolioId)
        {
            var p = await _mediator.Send(new GetPortfolio(portfolioId));
            return View(p.Transactions);
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

                        await _mediator.Send(new ImportStockQuotes(portfolioId, stream));
                    }
                }
            }
            return RedirectToAction("Index", new { portfolioId });
        }
    }
}