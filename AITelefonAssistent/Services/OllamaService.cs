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


       /* 
       public async Task<string> GetModelsAsync()
       {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/tags");

            return await response.Content.ReadAsStringAsync();
       }
       */ 


        
        public async Task<string> GetResponseAsync(string message)
        {
            var request = new{ model = "qwen3:8b", messages = new[]{ new {role = "user",content = message}},stream = false};// Im Gegensat zu Objekt response handelt es sich hier um ein anonymes Objekt, das die Anfrage an den Ollama-Server darstellt. 
                                                                                                                            //Es enthält Informationen über das Modell, die Nachrichten und den Stream-Modus.


            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/chat", request);//api/chat bedeutet, dass es sich um eine Chat-Anfrage handelt,
                                                                                                   //die an den Ollama-Server gesendet wird. Der Endpunkt /api/chat ist für die Verarbeitung von Chat-Nachrichten zuständig.

            // return await response.Content.ReadAsStringAsync();




            OllamaResponse result = await response.Content.ReadFromJsonAsync<OllamaResponse>();

            return result.Message.Content;
        }
        
    }
}