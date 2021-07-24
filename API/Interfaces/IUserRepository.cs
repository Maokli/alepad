using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserById(int userId);
    }
}