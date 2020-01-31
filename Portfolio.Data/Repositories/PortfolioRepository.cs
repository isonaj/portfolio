using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data.Repositories
{
    public class PortfolioRepository : IRepository<Model.Portfolio>
    {
        PortfolioDbContext _db;
        public PortfolioRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public async Task<Model.Portfolio> Get(Guid id)
        {
            return await _db.Portfolios.Include("Transactions")
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Model.Portfolio>> GetAll()
        {
            return await _db.Portfolios.Include("Transactions")
                .ToListAsync();
        }

        public async Task Create(Model.Portfolio entity)
        {
            _db.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Save(Model.Portfolio entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var portfolio = await _db.Portfolios.Where(p => p.Id == id).SingleOrDefaultAsync();
            _db.Portfolios.Remove(portfolio);
            await _db.SaveChangesAsync();
        }
    }
}
