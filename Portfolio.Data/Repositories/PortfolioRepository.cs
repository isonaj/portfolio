using Portfolio.Model;
using Portfolio.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portfolio.Data.Repositories
{
    public class PortfolioRepository : IRepository<Model.Portfolio>
    {
        PortfolioDbContext _db;
        public PortfolioRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public Model.Portfolio Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Portfolio> GetAll()
        {
            return _db.Portfolios.ToList();
        }

        public void Save(Model.Portfolio entity)
        {
            throw new NotImplementedException();
        }
    }
}
