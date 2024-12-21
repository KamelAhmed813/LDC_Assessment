using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserQueryService
    {
        private readonly ApplicationDBContext _context;

        public UserQueryService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int?> SaveQueryAsync(UserQuery query)
        {
            _context.userQueries.Add(query);
            await _context.SaveChangesAsync();
            return query.id;
        }

        public async Task<int> SaveResponceAsync(ChatBotResponse chatBotResponse){
            _context.chatBotResponses.Add(chatBotResponse);
            await _context.SaveChangesAsync();
            return chatBotResponse.id;
        }

        public async Task<String?> GetResponseAsync(int queryId)
        {
            ChatBotResponse? botResponse = await _context.chatBotResponses.FirstOrDefaultAsync(r => r.queryId == queryId);
            if(botResponse != null)
                return botResponse.response;
            else
                return null;
        }

        public async Task<Boolean> IsUserQuerySavedAsync(int queryId){
            UserQuery? query = await _context.userQueries.FindAsync(queryId);
            return !(query == null);
        }
    }
}