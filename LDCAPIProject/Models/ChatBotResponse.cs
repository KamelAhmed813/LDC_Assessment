using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDCAPIProject.Models
{
    public class ChatBotResponse
    {
        public int? id { get; set; }
        public String response { get; set; }
        public DateTime timestamp { get; set; }
        public int? queryID { get; set; }
        public UserQuery? query { get; set; }
    }
}