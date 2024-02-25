using Cards.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Cards.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
