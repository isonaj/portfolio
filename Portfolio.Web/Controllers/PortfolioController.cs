using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Model.Repositories;

namespace Portfolio.Web.Controllers
{
    public class PortfolioController : Controller
    {
        IRepository<Model.Portfolio> _repo;
        public PortfolioController(IRepository<Model.Portfolio> repo)
        {
            _repo = repo;
        }

        // GET: Portfolio
        public ActionResult Index()
        {
            var portfolios = _repo.GetAll();
            return View(portfolios);
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(Guid id)
        {
            var portfolio = _repo.Get(id);
            portfolio.GenerateSummary();
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var portfolio = new Model.Portfolio(Guid.NewGuid(), collection["Name"]);
                _repo.Create(portfolio);

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
                _repo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}