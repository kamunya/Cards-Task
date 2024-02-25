using Microsoft.AspNetCore.Identity;
using Cards.Core.Entities.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.Infrastracture.identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserData(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                AppUser appUser = new AppUser
                {
                    DisplayName = "Samwel Kamunya",
                    UserName = "Kamunya",
                    Email = "kamunya@cards.com",
                    Address = new Address
                    {
                        FirstName = "Samwel",
                        LastName = "Kamunya",
                        City = "Nairobi",
                        State = "Kenya",
                        ZipCode="00100",
                    }
                };
                await userManager.CreateAsync(appUser, "Pass@123!");

                appUser = new AppUser
                {
                    //Member
                    DisplayName = "Chana Kamunya",
                    UserName = "Chana",
                    Email = "chana@cards.com",
                    Address = new Address
                    {
                        FirstName = "Chana",
                        LastName = "Kamunya",
                        City = "Nairobi",
                        State = "Kenya",
                        ZipCode = "00100",
                    }
                };
                await userManager.CreateAsync(appUser, "Pass@123!");

                appUser = new AppUser
                {
                    //Member
                    DisplayName = "Netanel Kamunya",
                    UserName =  "Netanel",
                    Email = "netanel@cards.com",
                    Address = new Address
                    {
                        FirstName = "Netanel",
                        LastName = "Kamunya",
                        City = "Nairobi",
                        State = "Kenya",
                        ZipCode = "00100",
                    }
                };
                await userManager.CreateAsync(appUser, "Pass@123!");
            }
        }
    }
}
