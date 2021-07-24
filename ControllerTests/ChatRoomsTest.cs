using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ControllerTests
{
    public class ChatRoomsTest
    {
        [Fact]
        public async Task GetAllChatRooms_ReturnsOkResult_WithFourChatRooms()
        {
            //Arrange
            var mockRepo = new Mock<IChatRoomRepository>();
            var fakeChatRooms = new List<ChatRoom>{
                new ChatRoom{Name="General Realm"},
                new ChatRoom{Name="Chill Realm"},
                new ChatRoom{Name="Cherry Talks"},
                new ChatRoom{Name="Aries Palace"},
            };

            mockRepo.Setup(repo => repo.GetAllChatRooms())
                .Returns(Task.FromResult(fakeChatRooms.AsEnumerable()));

            var controller = new ChatRoomController(mockRepo.Object);

            //Act
            var result = await controller.GetAllChatRooms();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var data = Assert.IsAssignableFrom<IEnumerable<ChatRoom>>(okResult.Value);

            Assert.Equal(4,data.Count());
        }

        [Fact]
        public async Task GetChatRoomById_ReturnsOkResult_WithChatRoom()
        {
            //Arrange
            var mockRepo = new Mock<IChatRoomRepository>();
            mockRepo.Setup(repo=> repo.GetChatRoomById(4))
                .Returns(Task.FromResult(new ChatRoom{Id=4}));
            var controller = new ChatRoomController(mockRepo.Object);

            //Act
            var result = await controller.GetChatRoomById(4);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var data = Assert.IsType<ChatRoom>(okResult.Value);

            Assert.Equal(4,data.Id);
        }

        [Fact]
        public async Task GetChatRoomById_ReturnsNotFoundResult()
        {
            //Arrange
            var mockRepo = new Mock<IChatRoomRepository>();
            var controller = new ChatRoomController(mockRepo.Object);

            //Act
            var result = await controller.GetChatRoomById(10);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
