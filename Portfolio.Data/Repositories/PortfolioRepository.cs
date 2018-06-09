using Microsoft.EntityFrameworkCore;
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
            return _db.Portfolios.Include("Transactions")
                .Where(p => p.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<Model.Portfolio> GetAll()
        {
            return _db.Portfolios.Include("Transactions")
                .ToList();
        }

        public void Create(Model.Portfolio entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Save(Model.Portfolio entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var portfolio = _db.Portfolios.Where(p => p.Id == id).SingleOrDefault();
            _db.Portfolios.Remove(portfolio);
            _db.SaveChanges();
        }
    }
}
