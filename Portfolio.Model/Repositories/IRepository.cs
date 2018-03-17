using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Save(T entity);
    }
}
