using API.Interfaces;

namespace API.Services
{
    public class BotService(HttpClient httpClient) : IBotService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<string> GenerateResponse(string query){
            var url = "http://127.0.0.1:8090/ask";
            JsonContent content = JsonContent.Create(new {query = query});  
            HttpResponseMessage botModelResponse = await _httpClient.PostAsync(url, content);
            botModelResponse.EnsureSuccessStatusCode();
            return await botModelResponse.Content.ReadAsStringAsync();
        }
    }
}