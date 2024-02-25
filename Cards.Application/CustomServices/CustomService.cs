using Cards.Application.ICustomServices;
using Cards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.Application.CustomServices
{
    public class CustomService : ICustomService<Card>
    {
        public void Delete(Card entity)
        {
            try
            {
              

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Card entity)
        {
            throw new NotImplementedException();
        }

        public Task<Card> Update(Card entity)
        {
            throw new NotImplementedException();
        }
    }
}
