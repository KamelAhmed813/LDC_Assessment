using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserQuery
    {
        public int id { get; set; }
        public String query { get; set; }
        public DateTime timestamp { get; set; }
        public ChatBotResponse? response { get; set; }
    }
}