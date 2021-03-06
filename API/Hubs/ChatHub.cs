using System;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using API.Extensions;

namespace API.Hubs
{
  [Authorize]
  public class ChatHub : Hub
  {
    private readonly IMessageRepository _messageRepository;
    public ChatHub(IMessageRepository messageRepository)
    {
      _messageRepository = messageRepository;
    }

    public async override Task OnConnectedAsync()
    {
      var httpContext = Context.GetHttpContext();
      var roomId = httpContext.Request.Query["roomId"].ToString();

      await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

      var messages = await _messageRepository.GetChatRoomMessagesWithUser(Int16.Parse(roomId));

      var messagesToReturn = messages.Select(m => new MessageToReturnDto{
            Content = m.Content,
            SenderUserName = m.Sender.UserName,
        });

      await Clients.Group(roomId).SendAsync("RecievedMessages",messagesToReturn);
    }

    public async Task SendMessage(CreateMessageDto createMessageDto)
    {
        int userId = Context.GetHttpContext().User.GetUserId();
        string userName = Context.GetHttpContext().User.GetUserName();

        _messageRepository.AddMessage(createMessageDto, userId);

        if(await _messageRepository.SaveAllAsync())
        {
            var httpContext = Context.GetHttpContext();
            var roomId = httpContext.Request.Query["roomId"].ToString();

            var messageToReturn = new MessageToReturnDto{
                SenderUserName = userName,
                Content = createMessageDto.Content,
            };

            await Clients.Group(roomId).SendAsync("NewMessage", messageToReturn);
        }
        else
            throw new HubException("something went wrong");
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
      await base.OnDisconnectedAsync(exception);
    }

  }
}