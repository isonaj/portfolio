using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Portfolio.Application.StockQuote;

namespace Portfolio.Web.Controllers
{
    public class StockController : Controller
    {
        IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportFiles(List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        await _mediator.Send(new ImportStockQuotes(stream));
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}