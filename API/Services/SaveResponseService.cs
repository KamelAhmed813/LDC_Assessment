using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Services
{
    public class SaveResponseService(ApplicationDBContext context) : ISaveResponseService
    {
        private readonly ApplicationDBContext _context=context;
        public async Task<bool> SaveResponceAsync(string response, int queryId){
            _context.ChatBotResponses.Add(
                new ChatBotResponse{
                    response=response, 
                    queryId=queryId
                    }
                );
            await _context.SaveChangesAsync();
            return true;
        }
    }
}