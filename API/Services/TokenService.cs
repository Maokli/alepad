using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using API.Models.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config)
    {
      _config = config;
    }

    public string GenerateToken(UserAuthDto userAuthDto, int userId)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config["TokenKey"]);

      var tokenDescriptor = new SecurityTokenDescriptor{
          Subject = 
          new ClaimsIdentity(new[] {
            new Claim("userName",userAuthDto.UserName),
            new Claim("userId",userId.ToString()),
            }),
          Expires = DateTime.UtcNow.AddDays(7),
          SigningCredentials = new SigningCredentials(
              new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha256Signature
              )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}