using E2EEMessenger.Models;

namespace E2EEMessenger.Services
{
    public interface IMessageService
    {
        User CreateUser(CreateUserDto dto);
        Conversation CreateConversation();
        Message SendMessage(SendMessageDto dto);
        IEnumerable<Message> GetMessageHistory(string conversationId);
    }
}