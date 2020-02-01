using Portfolio.Application.Data;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        PortfolioDbContext _db;
        public UnitOfWork(PortfolioDbContext db)
        {
            _db = db;
        }

        public IRepository<T> Repository<T>() where T: class
        {
            return new Repository<T>(_db);
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
