using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class TestController : BaseApiController
  {
    private readonly DataContext _context;
    public TestController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> Test()
    {
      var names = new List<string>{
        "General Realm",
        "Chill Realm",  
        "Cherry Talks",  
        "Aries Palace",  
      };
      foreach(string name in names){
          var chatRoom = new ChatRoom{
              Name = name
          };
          await _context.ChatRooms.AddAsync(chatRoom);
          await _context.SaveChangesAsync();
      }
      return Ok(_context.ChatRooms.ToListAsync());
    }
  }
}