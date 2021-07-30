using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
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
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<LoginDto>> Signup(UserAuthDto userAuthDto)
    {
      if(await UserExists(userAuthDto.UserName)) 
        return BadRequest("Username is taken");

      int Id = GetLastId()+1;

      var user = new AppUser {
          UserName = userAuthDto.UserName,
          Id = Id
      };

      var result = await _userManager.CreateAsync(user, userAuthDto.Password);

      if(!result.Succeeded) return BadRequest(result.Errors);

      return new LoginDto{
          UserName = userAuthDto.UserName,
          Token = _tokenService.GenerateToken(userAuthDto),
          Id = Id
      };
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
            Token = _tokenService.GenerateToken(userAuthDto),
            Id = user.Id
        };
    }
    private async Task<bool> UserExists(string userName) {
        return await _userManager.Users
            .AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
    }

    private int GetLastId()
    {
      return _userManager.Users.OrderBy(u => u.Id).LastAsync().Id;
    }
  }

}