using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class MessageController : BaseApiController
  {
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
    {
      _messageRepository = messageRepository;
      _userRepository = userRepository;
    }
    public MessageController(IMessageRepository messageRepository)
    {
      _messageRepository = messageRepository;
    }

    [HttpGet("{chatRoomId}")]
    public async Task<ActionResult<MessageToReturnDto>> GetChatRoomMessages(int chatRoomId)
    {
        var messages = await _messageRepository.GetChatRoomMessages(chatRoomId);


        var messagesToReturn = messages.Select(m => new MessageToReturnDto{
            Content = m.Content,
            DateSent = m.DateSent,
            SenderUserName = GetUsernameByUserId(m.SenderId),
        });

        return Ok(messagesToReturn);
    }

    [HttpPost]
    public async Task<ActionResult> AddMessage(CreateMessageDto createMessageDto)
    {
        _messageRepository.AddMessage(createMessageDto);

        var succeeded = await _messageRepository.SaveAllAsync();

        if(!succeeded) return BadRequest("Something went wrong");

        return Ok("Message Sent");
    }

    private string GetUsernameByUserId(int userId)
    {
        var user = _userRepository.GetUserById(userId);

        return user.Result.UserName;
    } 
  }
}