using E2EEMessenger.Models;
using Microsoft.EntityFrameworkCore;

namespace E2EEMessenger.Data
{
    public class MessengerContext : DbContext
    {
        public MessengerContext(DbContextOptions<MessengerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}