using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Services
{
    public class SaveQueryService(ApplicationDBContext context) : ISaveQueryService
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<int> SaveQueryAsync(string query) 
        {
            UserQuery queryModel = new UserQuery{query = query};
            _context.UserQueries.Add(queryModel);
            await _context.SaveChangesAsync();
            return queryModel.id;
        }
    }
}