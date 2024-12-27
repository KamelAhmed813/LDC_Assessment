namespace API.Interfaces
{
    public interface IBotService
    {
        Task<string> GenerateResponse(string query);
    }
}