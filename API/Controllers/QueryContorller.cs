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
                try{
                    HttpResponseMessage botCreateResponse = await _httpClient.PostAsync(url, content);
                    botCreateResponse.EnsureSuccessStatusCode();
                    return Ok(await botCreateResponse.Content.ReadAsStringAsync());
                }catch(Exception e){
                    return StatusCode(400, "Query saved to DB but following error occured while generatinng response\n"+e.ToString());
                }
            }
            else{
                return StatusCode(400, "Error Occured while saving query to DB");
            }
        }
    }
}