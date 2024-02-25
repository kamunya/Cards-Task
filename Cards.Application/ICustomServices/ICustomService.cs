using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.Application.ICustomServices
{
    public interface ICustomService<T>
    {
        IEnumerable<T> GetAll();
        void FindById(int Id);
        void Insert(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}
