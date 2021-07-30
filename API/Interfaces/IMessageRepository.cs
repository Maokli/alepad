using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(CreateMessageDto createMessageDto, int userId);
        Task<IEnumerable<Message>> GetChatRoomMessagesWithUser(int chatroomId);
        Task<bool> SaveAllAsync();
    }
}