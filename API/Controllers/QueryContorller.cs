using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("query")]
    [ApiController]
    public class QueryContorller(IBusinessLogicService businessLogic) : ControllerBase
    {
        private readonly IBusinessLogicService _businessLogic=businessLogic;

        [HttpPost]
        public async Task<ActionResult<string>> UserQuery([FromBody]string query){
            try{
                string response = await _businessLogic.ProcessUserQueryAsync(query);
                return Ok(response);
            }
            catch(DbUpdateException e){
                return StatusCode(400, $"Database error occured:\n{e.InnerException?.Message}");
            }
            catch(HttpRequestException e){
                return StatusCode(400, $"Error occured while communicating with the bot model:\n{e.ToString()}");
            }
            catch(Exception e){
                return StatusCode(500, $"Error occured while generatinng response:\n{e.InnerException?.ToString()}");
            }
        }
    }
}