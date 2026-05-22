using E2EEMessenger.Data;
using E2EEMessenger.Models;

namespace E2EEMessenger.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessengerContext _context;

        public MessageService(MessengerContext context)
        {
            _context = context;
        }

        public User CreateUser(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.PublicKey))
                throw new ArgumentException("Name and PublicKey cannot be empty.");

            var user = new User { Name = dto.Name, PublicKey = dto.PublicKey };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public Conversation CreateConversation()
        {
            var conversation = new Conversation();
            _context.Conversations.Add(conversation);
            _context.SaveChanges();
            return conversation;
        }

        public Message SendMessage(SendMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ciphertext))
                throw new ArgumentException("Empty messages are not allowed.");

            if (!_context.Users.Any(u => u.Id == dto.SenderId))
                throw new ArgumentException("Sender does not exist.");

            if (!_context.Conversations.Any(c => c.Id == dto.ConversationId))
                throw new ArgumentException("Conversation does not exist.");

            var message = new Message
            {
                ConversationId = dto.ConversationId,
                SenderId = dto.SenderId,
                Ciphertext = dto.Ciphertext
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public IEnumerable<Message> GetMessageHistory(string conversationId)
        {
            if (!_context.Conversations.Any(c => c.Id == conversationId))
                throw new ArgumentException("Conversation does not exist.");

            return _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.CreatedAt)
                .ToList();
        }
    }
}