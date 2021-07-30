using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class MessageRepository : IMessageRepository
  {
    private readonly DataContext _context;
    public MessageRepository(DataContext context)
    {
      _context = context;
    }

  public void AddMessage(CreateMessageDto createMessageDto, int userId)
  {
    var message = new Message
    {
      SenderId = userId,
      ChatRoomId = createMessageDto.ChatRoomId,
      Content = createMessageDto.Content
    };

    _context.Messages.Add(message);
  }

  public async Task<IEnumerable<Message>> GetChatRoomMessagesWithUser(int chatroomId)
  {
    return await _context.Messages
      .Include(m => m.Sender)
      .Where(m => m.ChatRoomId == chatroomId)
      .OrderBy(m => m.Id)
      .ToListAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}
}