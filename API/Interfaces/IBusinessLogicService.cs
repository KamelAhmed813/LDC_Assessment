namespace API.Interfaces
{
    public interface IBusinessLogicService
    {
        Task<string> ProcessUserQueryAsync(string query);
    }
}