namespace API.Interfaces
{
    public interface ISaveResponseService
    {
        Task<bool> SaveResponceAsync(string response, int queryId);
    }
}