using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CreateResponseDto
    {
        public required String query { get; set; }
        public required int queryId { get; set; }
    }
}