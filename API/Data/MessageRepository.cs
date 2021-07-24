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

  public void AddMessage(CreateMessageDto createMessageDto)
  {
    var message = new Message
    {
      SenderId = createMessageDto.SenderId,
      ChatRoomId = createMessageDto.ChatRoomId,
      Content = createMessageDto.Content
    };

    _context.Messages.Add(message);
  }

  public async Task<IEnumerable<Message>> GetChatRoomMessages(int chatroomId)
  {
    return await _context.Messages
      .Where(m => m.ChatRoomId == chatroomId)
      .ToListAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}
}