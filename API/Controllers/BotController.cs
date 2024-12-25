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
        private readonly UserQueryService _userQueryService;
        private readonly HttpClient _httpClient;
        public BotController(UserQueryService userQuery, HttpClient httpClient){
            _userQueryService = userQuery;
            _httpClient = httpClient;
        }
        /*
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
        */

        [HttpPost("generateResponse")]
        public async Task<ActionResult<String>> askBot([FromBody]CreateResponseDto createRequest){
            var url = "http://127.0.0.1:8080/ask";
            JsonContent content = JsonContent.Create(createRequest);
            try{
                HttpResponseMessage botModelResponse = await _httpClient.PostAsync(url, content);
                botModelResponse.EnsureSuccessStatusCode();
                String botModelResponseBody = await botModelResponse.Content.ReadAsStringAsync();
                int? responseId = await _userQueryService.SaveResponceAsync(
                    new ChatBotResponse{
                        response = botModelResponseBody,
                        queryId = createRequest.queryId
                    }
                );
                if(responseId != null)
                    return Ok(botModelResponseBody);
                else
                    return StatusCode(400, "Error Occured while trying to save bot response to DB");
            }catch(Exception e){
                return StatusCode(500, "Some error occured\n"+e.ToString());
            }
        }
            /*
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
                return Ok(response);
            else
                return StatusCode(400, "Error Occured while trying to save bot response to DB");
            }
            */
    }
}