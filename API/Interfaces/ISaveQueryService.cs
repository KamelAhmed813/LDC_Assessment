namespace API.Interfaces
{
    public interface ISaveQueryService
    {
        Task<int> SaveQueryAsync(string query);
    }
}