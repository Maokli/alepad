namespace API.Models.Dtos
{
    public class CreateMessageDto
    {
        public string Content { get; set; }
        public int ChatRoomId { get; set; }
    }
}