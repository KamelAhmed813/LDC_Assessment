using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("API/query")]
    [ApiController]
    public class QueryContorller : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly UserQueryService _userQueryService;
        public QueryContorller(HttpClient httpClient, UserQueryService userQueryService){
            _httpClient = httpClient;
            _userQueryService = userQueryService;
        }

        [HttpPost]
        public async Task<ActionResult<String>> UserQuery([FromBody]UserQueryDto userQuery){
            int? queryId = await _userQueryService.SaveQueryAsync(
                new UserQuery{
                    query = userQuery.query
                }
            );
            if(queryId != null){
                var url = $"{Request.Scheme}://{Request.Host}/API/Bot/generateResponse";
                JsonContent content = JsonContent.Create(
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