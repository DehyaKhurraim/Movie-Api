using JwtAuthenticationManager.Models;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = _jwtTokenHandler.GenerateJwtToken(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}
