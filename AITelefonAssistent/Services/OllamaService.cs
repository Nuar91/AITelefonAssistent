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

        public async Task<string> GetModelsAsync()
        {
            var response = await _httpClient.GetAsync("/api/tags");

            return await response.Content.ReadAsStringAsync();
        }
    }
}