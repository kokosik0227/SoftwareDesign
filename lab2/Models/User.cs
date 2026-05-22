namespace E2EEMessenger.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        // Зберігаємо лише публічний ключ (суть варіанту 8)
        public string PublicKey { get; set; } = string.Empty;
    }
}