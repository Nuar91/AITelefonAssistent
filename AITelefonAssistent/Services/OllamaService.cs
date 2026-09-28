using System.Net.Http.Json;

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
            var request = new
            {
                model = "qwen3:8b",
                messages = messages,
                stream = false
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/chat", request);

            OllamaResponse result = await response.Content.ReadFromJsonAsync<OllamaResponse>();

            return result.Message.Content;
        }

    }
}