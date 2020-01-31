using Portfolio.Model;
using System.Threading.Tasks;

namespace Portfolio.Application.Data
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T: Entity;

        Task SaveChanges();
    }
}
