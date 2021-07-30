using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public string Content { get; set; }
        public int ChatRoomId { get; set; }
    }
}