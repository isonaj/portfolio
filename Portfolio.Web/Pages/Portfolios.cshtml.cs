using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Model.Repositories;

namespace Portfolio.Web.Pages
{
    public class PortfoliosModel : PageModel
    {
        public IEnumerable<Model.Portfolio> Portfolios { get; private set; }
        IRepository<Model.Portfolio> _repo;
        public PortfoliosModel(IRepository<Model.Portfolio> repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            Portfolios = _repo.GetAll();
        }
    }
}