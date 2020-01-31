using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task Save(T entity);
        Task Create(T entity);
        Task Delete(Guid id);
    }
}
