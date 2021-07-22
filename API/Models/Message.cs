using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public IdentityUser Sender { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}