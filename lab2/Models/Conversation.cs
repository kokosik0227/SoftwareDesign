namespace E2EEMessenger.Models
{
    public class Conversation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; } = "direct";
    }
}