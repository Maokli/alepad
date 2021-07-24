using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<AppUser> GetUserById(int userId)
    {
      return await _context.Users
        .SingleOrDefaultAsync(u => u.Id == userId);

    }
  }
}