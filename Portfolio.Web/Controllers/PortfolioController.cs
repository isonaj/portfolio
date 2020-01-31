using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Portfolio;
using Portfolio.Application.Data;

namespace Portfolio.Web.Controllers
{
    public class PortfolioController : Controller
    {
        IMediator _mediator;
        public PortfolioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Portfolio
        public async Task<ActionResult> Index()
        {
            var portfolios = await _mediator.Send(new GetPortfolios()); 
            return View(portfolios);
        }

        // GET: Portfolio/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var portfolio = await _mediator.Send(new GetPortfolio(id));
            return View(portfolio);
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                await _mediator.Send(new CreatePortfolio(Guid.NewGuid(), collection["Name"]));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Portfolio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Portfolio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Portfolio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _mediator.Send(new DeletePortfolio(id));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}