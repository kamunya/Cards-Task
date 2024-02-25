using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cards.Core.Entities.Identity;
using Cards.Core.Interfaces;
using Cards.Dtos;
using Cards.Errors;
using System.Threading.Tasks;

namespace Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new APIResponce(401));
  
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return Unauthorized(new APIResponce(401));
            return new UserDto
            {
                
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };     
        }
    }
}
