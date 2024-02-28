using Microsoft.AspNetCore.Mvc;
using my_budget.web.Services;

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

        [HttpGet("GetMyAccountValueByStreaming")]
        public async Task<IActionResult> GetMyAccountValueByStreaming()
        {
            try
            {
                //var response = await _xtbService.GetMyAccountValueByStreaming();
                var response = _xtbService.GetCurrentBalance();
                return Ok(new { Balance = response.ToString() });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }

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