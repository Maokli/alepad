namespace API.Models.Dtos
{
    public class CreateMessageDto
    {
        public int SenderId { get; set; }
        public string Content { get; set; }
        public int ChatRoomId { get; set; }
    }
}