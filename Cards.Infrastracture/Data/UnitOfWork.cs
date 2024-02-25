using Cards.Core.Entities;
using Cards.Core.Interfaces;
using Cards.Infrastracture.Repositories;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Cards.Infrastracture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CardsContext _cardsContext;
        private Hashtable _repositories;

        public UnitOfWork(CardsContext CardsContext)
        {
            _cardsContext = CardsContext;
        }
        public async Task<int> Complete()
        {
            return await _cardsContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _cardsContext.Dispose();
        }

        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null ) _repositories= new Hashtable();
            var Type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)),_cardsContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }
    }
}
