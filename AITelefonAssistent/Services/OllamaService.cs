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



        /*
        public async Task<string> GetModelsAsync()
        {
            var response = await _httpClient.GetAsync("/api/tags");

            return await response.Content.ReadAsStringAsync();
        }*/



        public async Task<string> GetResponseAsync(string message)
        {
            var request = new
            {
                model = "qwen3:8b",
                messages = new[]
                {
            new
            {
                role = "user",
                content = message
            }
        },
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("/api/chat", request);

            return await response.Content.ReadAsStringAsync();
        }
    }
}