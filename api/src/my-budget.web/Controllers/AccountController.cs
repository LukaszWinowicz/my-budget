using Microsoft.AspNetCore.Mvc;
using my_budget.web.Models;
using my_budget.web.Services;

namespace my_budget.web.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IXtbService _xtbService;
        public AccountController(IXtbService xtbService)
        {
            _xtbService = xtbService;
        }

        [HttpPost("LoginToXtb")]
        public IActionResult LeginToXtb([FromBody] LoginModel loginModel)
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

    }
}
