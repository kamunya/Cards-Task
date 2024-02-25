using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Cards.Core.Entities.Identity
{
    public class AppUser :IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
