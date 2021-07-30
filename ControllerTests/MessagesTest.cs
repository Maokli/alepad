using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Interfaces;
using API.Models;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ControllerTests
{
    public class MessagesTest
    {
        [Fact]
        public async Task AddMessage_ReturnsOkResult()
        {
            //Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();


            mockMessageRepo.Setup(repo=> repo.SaveAllAsync())
                .ReturnsAsync(true);

            var fakeUserPrincipal = 
                new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("userName",""),
                    new Claim("userId","0"),
                }, "mock"));
            
            var controller = new MessageController(mockMessageRepo.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = fakeUserPrincipal }
            };
            
            //Act
            var result = await controller.AddMessage(new CreateMessageDto());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddMessage_ReturnsBadRequest()
        {
            //Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            
            mockMessageRepo.Setup(repo => repo.SaveAllAsync())
                .ReturnsAsync(false);

            var fakeUserPrincipal = 
                new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("userName",""),
                    new Claim("userId","0"),
                }, "mock"));
            
            var controller = new MessageController(mockMessageRepo.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = fakeUserPrincipal }
            };

            //Act
            var result = await controller.AddMessage(new CreateMessageDto());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddMessage_ReturnsUnauthorized()
        {
            //Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            
            var controller = new MessageController(mockMessageRepo.Object);

            //Act
            var result = await controller.AddMessage(new CreateMessageDto());

            //Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetChatRoomMessages_ReturnsOkResult()
        {
            //Arrange
            var fakeMessages = new List<Message>{
                new Message{
                    SenderId = 1, 
                    Sender = new AppUser{UserName="Jhon"}}
            };
            
            var mockMessageRepo = new Mock<IMessageRepository>();

            mockMessageRepo.Setup(repo => repo.GetChatRoomMessagesWithUser(1))
                .ReturnsAsync(fakeMessages);

            
            var controller = 
                new MessageController(mockMessageRepo.Object);
            
            //Act
            var result = await controller.GetChatRoomMessages(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var data = Assert.IsAssignableFrom<IEnumerable<MessageToReturnDto>>(okResult.Value);
            
            Assert.Equal("Jhon",data.First().SenderUserName);
        }
    }
}