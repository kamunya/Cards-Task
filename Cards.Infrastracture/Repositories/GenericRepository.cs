using Microsoft.EntityFrameworkCore;
using Cards.Core.Entities;
using Cards.Core.Interfaces;
using Cards.Core.Specifications;
using Cards.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CardsContext _CardsContext;

        public GenericRepository(CardsContext CardsContext)
        {
            _CardsContext = CardsContext;
        }
        public void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _CardsContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _CardsContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
        //Specification Pattern
        public async Task<T> GetEntityWithSpec(ISpecifications<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specification)
        {
           return await ApplySpecification(specification).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationEvaluatOr<T>.GetQuery(_CardsContext.Set<T>().AsQueryable(), specifications);
        }

        public void Add(T entity)
        {
            _CardsContext.Add<T>(entity);
        }

        public void Update(T entity)
        {
            _CardsContext.Attach<T>(entity);
            _CardsContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _CardsContext.Set<T>().Remove(entity);
        }
    }
}
