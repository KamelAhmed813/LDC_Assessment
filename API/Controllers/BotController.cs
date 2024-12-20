using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Dtos;
using System.Net;

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
        public BotController(HttpClient httpClient){
            _httpClient = httpClient;
        }

        [HttpGet("getResponse")]
        public async Task<ActionResult<String>> getResponse([FromBody]SearchResponseDto searchResponseDto){
            var url = $"{Request.Scheme}://{Request.Host}/API/DB/getBotResponse/{searchResponseDto.queryId}";
            HttpResponseMessage getBotResponse = await _httpClient.GetAsync(url);

            if(getBotResponse.StatusCode.Equals(HttpStatusCode.OK)){
                return Ok(await getBotResponse.Content.ReadAsStringAsync());
            }
            else{
                url = $"{Request.Scheme}://{Request.Host}/API/DB/getQuery/{searchResponseDto.queryId}";
                HttpResponseMessage getQueryResponse = await _httpClient.GetAsync(url);

                if(getQueryResponse.StatusCode.Equals(HttpStatusCode.OK))
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

            var url = $"{Request.Scheme}://{Request.Host}/API/DB/saveBotResponse";
                JsonContent content = JsonContent.Create(
                    new saveResponseDto{
                        response = response,
                    queryId = createRequest.queryId
                    }
                );
            HttpResponseMessage saveBotResponse = await _httpClient.PostAsync(url, content);

            if(saveBotResponse.StatusCode.Equals(HttpStatusCode.OK))
                return Ok("Your query response saved in DB");
            else
                return StatusCode(400, "Error Occured while trying to save bot response to DB");
            }
    }
}