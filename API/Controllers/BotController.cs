using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Dtos;
using System.Net;
using API.Services;
using API.Models;

namespace API.Controllers
{
    /*
    --Post Query -> Send it to python Model to process and response with OK [Done but always response with OK]
    --When Python Model finish processing Save response to DB with QueryID [Done normal method communicate with DB]
    --Get Query -> search (response DB with QueryId)    [Done]
        if found return response 
        if not (search query DB)
        if query found return still processing 
        if not return problem processing query
    */
    [Route("API/Bot")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly UserQueryService _userQueryService;
        public BotController(HttpClient httpClient, UserQueryService userQuery){
            _httpClient = httpClient;
            _userQueryService = userQuery;
        }

        [HttpGet("getResponse/{queryId}")]
        public async Task<ActionResult<String>> getResponse(int queryId){
            String? botResponse = await _userQueryService.GetResponseAsync(queryId);

            if(botResponse != null){
                return Ok(botResponse);
            }
            else{
                if(await _userQueryService.IsUserQuerySavedAsync(queryId))
                    return Ok("Your Query is still processing");
                else
                    return StatusCode(400, "There was error processing this query please send new one");
            }
        }

        [HttpPost("generateResponse")]
        public async Task<ActionResult<String>> askBot([FromBody]CreateResponseDto createRequest){
            //Have both query and queryId
            //Send to python Model the query and wait for the response
            //Create SaveResponse Object with the response and the queryId
            String response = "Python processed the query and this is a sample response";

            int? responseId = await _userQueryService.SaveResponceAsync(
                new ChatBotResponse{
                    response = response,
                    queryId = createRequest.queryId
                }
            );

            if(responseId != null)
                return Ok($"Your query response saved in DB with ID : {responseId}");
            else
                return StatusCode(400, "Error Occured while trying to save bot response to DB");
            }
    }
}