using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AuthController : BaseApiController
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<LoginDto>> Signup(UserAuthDto userAuthDto)
    {
      if(await UserExists(userAuthDto.UserName)) 
        return BadRequest("Username is taken");

      var user = new AppUser {
          UserName = userAuthDto.Password
      };

      var result = await _userManager.CreateAsync(user, userAuthDto.Password);

      if(!result.Succeeded) return BadRequest(result.Errors);

      return new LoginDto{
          UserName = userAuthDto.UserName,
          Token = "Not Implemented Yet"
      };
    }

    private async Task<bool> UserExists(string userName) {
        return await _userManager.Users
            .AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginDto>> Login(UserAuthDto userAuthDto){
        var user = await _userManager.Users
            .SingleOrDefaultAsync(
                u => u.UserName.ToLower() == userAuthDto.UserName.ToLower());

        if(user == null) return BadRequest("Invalid Username");

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, userAuthDto.Password, false);
        
        if(!result.Succeeded) return Unauthorized("Wrong Password");

        return new LoginDto{
            UserName = user.UserName,
            Token = "Not Implemented yet"
        };
    }
  }
}