using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_budget.web.Models;
using my_budget.web.Services;
using xAPI.Commands;
using xAPI.Responses;
using xAPI.Sync;

namespace my_budget.web.Controllers
{
    [Route("api/xtb")]
    [ApiController]
    public class XtbController : ControllerBase
    {
        private readonly IXtbService _xtbService;
        public XtbController(IXtbService xtbService)
        {
            _xtbService = xtbService;
        }

        [HttpPost("Login")]
        public IActionResult Legin([FromBody] LoginModel loginModel) 
        {
            
            try
            {
                var response = _xtbService.Login(loginModel);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }

        [HttpGet("GetMyTrades")]
        public IActionResult GetMyTrades()
        {
            try
            {
                var response = _xtbService.GetMyTrades();
                return Ok(response.TradeRecords);
            }
            catch (Exception ex)
            {

                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }

        //[HttpGet]
        //public IActionResult GetMyAccountValue() 
        //{
        //    try
        //    {
        //        var response = _xtbService.GetMyAccountValue();
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        [HttpGet("GetAllSymbols")]
        public IActionResult GetAllSymbols()
        {
            try
            {
                var response = _xtbService.GetAllSymbols();                
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }
    }
}
