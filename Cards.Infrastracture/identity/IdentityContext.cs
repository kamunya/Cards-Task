using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cards.Core.Entities.Identity;

namespace Cards.Infrastracture.identity
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        public DbSet<Address> Address { get; set; }

        protected IdentityContext()
        {
        }
    }
}
