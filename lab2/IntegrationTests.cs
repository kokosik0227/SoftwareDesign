using System.Net.Http.Json;
using E2EEMessenger.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace lab2
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Явно вказуємо правильний шлях до папки проєкту, щоб уникнути DirectoryNotFoundException
            var projectDir = AppDomain.CurrentDomain.BaseDirectory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot(projectDir);
            }).CreateClient();
        }

        [Fact]
        public async Task Test_Full_E2EE_Message_Flow()
        {
            // 1. Create User Alice
            var userResp = await _client.PostAsJsonAsync("/api/users", new CreateUserDto
            {
                Name = "Alice",
                PublicKey = "ssh-rsa AAAAB3NzaC1yc2E..."
            });
            Assert.True(userResp.IsSuccessStatusCode);
            var user = await userResp.Content.ReadFromJsonAsync<User>();
            Assert.NotNull(user);

            // 2. Create Conversation
            var convResp = await _client.PostAsync("/api/conversations", null);
            Assert.True(convResp.IsSuccessStatusCode);
            var conv = await convResp.Content.ReadFromJsonAsync<Conversation>();
            Assert.NotNull(conv);

            // 3. Send Encrypted Message
            var msgResp = await _client.PostAsJsonAsync("/api/messages", new SendMessageDto
            {
                ConversationId = conv.Id,
                SenderId = user.Id,
                Ciphertext = "0xABC123INTEGRATION_TEST_CIPHER"
            });
            Assert.True(msgResp.IsSuccessStatusCode);

            // 4. Get History and Verify
            var historyResp = await _client.GetAsync($"/api/conversations/{conv.Id}/messages");
            Assert.True(historyResp.IsSuccessStatusCode);

            var messages = await historyResp.Content.ReadFromJsonAsync<List<Message>>();
            Assert.NotNull(messages);
            Assert.Single(messages);
            Assert.Equal("0xABC123INTEGRATION_TEST_CIPHER", messages[0].Ciphertext);
            Assert.Equal(user.Id, messages[0].SenderId);
        }
    }
}