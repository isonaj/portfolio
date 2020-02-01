using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Data
{
    public interface IRepository<T> where T: class
    {
        void Add(T obj);
        IQueryable<T> AsQueryable();
    }
}
