using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class Repository<T> : IRepository<T> where T: class
    {
        PortfolioDbContext _db;
        public Repository(PortfolioDbContext db)
        {
            _db = db;
        }

        public void Add(T obj)
        {
            _db.Set<T>().Add(obj);
        }

        public IQueryable<T> AsQueryable()
        {
            return _db.Set<T>().AsQueryable();
        }
    }
}
