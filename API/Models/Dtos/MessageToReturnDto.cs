using System;

namespace API.Models.Dtos
{
    public class MessageToReturnDto
    {
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
        public string SenderUserName { get; set; }
        
    }
}