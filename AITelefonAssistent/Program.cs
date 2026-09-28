
using AITelefonAssistent.Services;
using Microsoft.AspNetCore.Mvc;





public class ChatRequest
{
    //  public string Message { get; set; }
    public List<ChatMessage> Messages { get; set; }
}

public class ChatMessage
{
    public string Role { get; set; }
    public string Content { get; set; }
}


namespace AITelefonAssistent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<OllamaService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();


            /*
            app.MapPost("/api/chat", ([FromBody] ChatRequest request) =>
            {
                return request.Message;
            });*/


            app.MapPost("/api/chat", async ([FromBody] ChatRequest request, OllamaService ollamaService) =>
            {
             //   string response = await ollamaService.GetResponseAsync(request.Message);
                string response = await ollamaService.GetResponseAsync(request.Messages);

                return response;
            });

            app.Run();
        }
    }
}
