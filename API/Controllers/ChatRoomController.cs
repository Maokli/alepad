using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace API.Controllers
{
  [Authorize]
  public class ChatRoomController : BaseApiController
  {
    private readonly IChatRoomRepository _chatRoomRepository;
    public ChatRoomController(IChatRoomRepository chatRoomRepository)
    {
      _chatRoomRepository = chatRoomRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ChatRoom>>> GetAllChatRooms() 
    {
        var chatRooms = await _chatRoomRepository.GetAllChatRooms();

        return Ok(chatRooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChatRoom>> GetChatRoomById(int id) 
    {
        var chatRoom = await _chatRoomRepository.GetChatRoomById(id);

        if(chatRoom == null) return NotFound();

        return Ok(chatRoom);
    }
  }
}