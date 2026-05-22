namespace E2EEMessenger.Models
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
    }

    public class SendMessageDto
    {
        public string ConversationId { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string Ciphertext { get; set; } = string.Empty;
    }
}