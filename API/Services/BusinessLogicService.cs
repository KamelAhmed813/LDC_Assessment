using API.Interfaces;

namespace API.Services
{
    public class BusinessLogicService(IBotService botService, ISaveQueryService saveQuery, ISaveResponseService saveResponse) : IBusinessLogicService
    {
        private readonly IBotService _botService = botService;
        private readonly ISaveQueryService _saveQuery = saveQuery;
        private readonly ISaveResponseService _saveResponse = saveResponse;
        public async Task<string> ProcessUserQueryAsync(string query)
        {
            int queryId = await _saveQuery.SaveQueryAsync(query);
            string response = await _botService.GenerateResponse(query);
            bool isResponseSaved = await _saveResponse.SaveResponceAsync(response, queryId);
            if (!isResponseSaved)
                throw new Exception("Failed to save response.");
            return response;
        }
    }
}