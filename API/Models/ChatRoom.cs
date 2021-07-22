using System.Collections.Generic;

namespace API.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}