using API.Models.Dtos;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserAuthDto userAuthDto, int userId);
    }
}