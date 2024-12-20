using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class saveResponseDto
    {
        public required String response { get; set; }
        public required int queryId { get; set; }
    }
}