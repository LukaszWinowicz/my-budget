using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xAPI.Commands;
using xAPI.Responses;
using xAPI.Sync;

namespace my_budget.web.Controllers
{
    [Route("api/xtb")]
    [ApiController]
    public class XtbController : ControllerBase
    {

        private static Server serverData = Servers.DEMO;
        private static SyncAPIConnector connector = new SyncAPIConnector(serverData);

        [HttpPost]
        public IActionResult Legin([FromBody] LoginModel loginModel) 
        {
            string appName = "firstApp";
            string appId = "";

            try
            {
                // Login to server
                Credentials credentials = new Credentials(loginModel.userId, loginModel.password, appId, appName);
                LoginResponse loginResponse = APICommandFactory.ExecuteLoginCommand(connector, credentials, true);

                // Login Response - StreamSessionId
                var StreamSessionId = loginResponse.StreamSessionId;
                return Ok(loginResponse);

            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }

        [HttpGet]
        public IActionResult GetMyTrades()
        {            
            TradesResponse tradesResponse = APICommandFactory.ExecuteTradesCommand(connector, true);
            return Ok(tradesResponse.TradeRecords);
        }

        public class LoginModel
        {
            public string userId { get; set; }
            public string password { get; set; }
        }
    }
}
