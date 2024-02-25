using Cards.Core.Entities.Identity;

namespace Cards.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
