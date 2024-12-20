using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("API/DB")]
    [ApiController]
    public class DBController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public DBController(ApplicationDBContext context){
            _context = context;
        }

        [HttpPost("saveUserQuery")]
        public int saveUserQuery([FromBody]UserQueryDto querybody){
            UserQuery userQuery = new UserQuery{
                query = querybody.query
            };
            _context.userQueries.Add(userQuery);
            _context.SaveChanges();
            return userQuery.id;
        }

        [HttpPost("saveBotResponse")]
        public int saveBotResponse([FromBody]saveResponseDto BotResponse){
            ChatBotResponse chatBotResponse = new ChatBotResponse{
                response = BotResponse.response,
                queryID = BotResponse.queryId
            };
            _context.chatBotResponses.Add(chatBotResponse);
            _context.SaveChanges();
            return chatBotResponse.id;
        }

        [HttpGet("getBotResponse/{queryId}")]
        public String? getBotResponseByQueryId([FromHeader]int queryID){
            return _context.chatBotResponses.Where(r => r.queryID == queryID).ToList().ToString();
        }

        [HttpGet("getQuery/{queryId}")]
        public String? getQueryById([FromHeader]int queryId){
            var queryObj = _context.userQueries.Find(queryId);
            if(queryObj == null)
                return null;
            else
                return queryObj.query;
        }
        
    }
}