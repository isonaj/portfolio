using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Data
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Model.Portfolio>> GetAll();
        Task<Model.Portfolio> Get(Guid id);
        Task Save(Model.Portfolio entity);
        Task Create(Model.Portfolio entity);
        Task Delete(Guid id);
    }
}
