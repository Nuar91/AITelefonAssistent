

namespace AITelefonAssistent.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:11434");
        }

        public class OllamaResponse
        {
            public OllamaMessage Message { get; set; }
        }

        public class OllamaMessage
        {
            public string Content { get; set; }
        }

        public async Task<string> GetResponseAsync(List<ChatMessage> messages)
        {
            ChatMessage systemMessage = new ChatMessage
            {
                Role = "system",
                Content = "Du bist ein Telefonassistent. Antworte kurz und natürlich, wie in einem echten Telefongespräch. Verwende keine Emojis, kein Markdown und keine Aufzählungen. Vermeide unnötige Erklärungen und beschränke dich normalerweise auf höchstens zwei bis drei Sätze."
            };

            List<ChatMessage> allMessages = new List<ChatMessage>();

            allMessages.Add(systemMessage);
            allMessages.AddRange(messages);

            var request = new
            {
                model = "qwen3:8b",
                messages = allMessages,
                stream = false
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/chat", request);

            OllamaResponse result = await response.Content.ReadFromJsonAsync<OllamaResponse>();

            return result.Message.Content;
        }
    }
}