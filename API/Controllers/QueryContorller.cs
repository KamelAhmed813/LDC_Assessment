using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("API/query")]
    [ApiController]
    public class QueryContorller : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public QueryContorller(HttpClient httpClient){
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<ActionResult<String>> UserQuery([FromBody]UserQueryDto userQuery){
            var url = $"{Request.Scheme}://{Request.Host}/API/DB/saveUserQuery";
                JsonContent content = JsonContent.Create(
                    new UserQueryDto{
                        query = userQuery.query
                    }
                );
            HttpResponseMessage saveQueryResponse = await _httpClient.PostAsync(url, content);

            if(saveQueryResponse.StatusCode.Equals(HttpStatusCode.OK)){
                int.TryParse(await saveQueryResponse.Content.ReadAsStringAsync(), out int queryId);
                url = $"{Request.Scheme}://{Request.Host}/API/Bot/generateResponse";
                content = JsonContent.Create(
                    new CreateResponseDto{
                        query = userQuery.query,
                        queryId = (int)queryId
                    }
                );
                HttpResponseMessage botCreateResponse = await _httpClient.PostAsync(url, content);
                if(botCreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                    return Ok("Query saved to DB and response generated and saved to DB");
                else
                    return Ok("Query saved to DB but error occured while generatinng response");
            }
            else{
                return StatusCode(400, "Error Occured while saving query to DB");
            }
        }
    }
}