using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class ChatRoomRepository : IChatRoomRepository
  {
    private readonly DataContext _context;

    public ChatRoomRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<ChatRoom>> GetAllChatRooms()
    {
      return await _context.ChatRooms
        .ToListAsync();
    }

    public async Task<ChatRoom> GetChatRoomById(int id)
    {
      return await _context.ChatRooms
        .SingleOrDefaultAsync(x => x.Id == id);
    }
  }
}