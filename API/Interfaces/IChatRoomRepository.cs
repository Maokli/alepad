using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IChatRoomRepository
    {
        Task<IEnumerable<ChatRoom>> GetAllChatRooms();
        Task<ChatRoom> GetChatRoomById(int id);
    }
}