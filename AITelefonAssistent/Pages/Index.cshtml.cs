using AITelefonAssistent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AITelefonAssistent.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly OllamaService _ollamaService;

        public IndexModel(ILogger<IndexModel> logger,OllamaService ollamaService)
        {
            _logger = logger;
            _ollamaService = ollamaService;
        }

        public string Begruessung { get; private set; }

        public async Task OnGetAsync()
        {
            Begruessung = await _ollamaService.GetModelsAsync();
        }
    }
}
