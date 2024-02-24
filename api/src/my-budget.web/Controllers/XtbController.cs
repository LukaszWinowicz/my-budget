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

        [HttpPost]
        public IActionResult Legin([FromBody] LoginModel loginModel) 
        {
            Server serverData = Servers.DEMO;

            string appName = "firstApp";
            string appId = "";

            try
            {
                // Connect to server
                SyncAPIConnector connector = new SyncAPIConnector(serverData);

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

        public class LoginModel
        {
            public long userId { get; set; }
            public string password { get; set; }
        }
    }
}
