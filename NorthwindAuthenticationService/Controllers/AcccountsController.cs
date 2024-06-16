using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NorthwindModelClassLibrary;

namespace NorthwindAuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AcccountsController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IUserServiceAsync _userService;
        public AcccountsController(IOptions<AppSettings> options, IUserServiceAsync userService)
        {
            _settings = options.Value;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.AuthenticateAsync(model);
            if(user is null)
            {
                return NotFound("Bad username/password");
            }
            var token = TokenManager.GenerateWebToken(user, _settings);
            var authResponse = new AuthenticationResponse(user, token);
            return authResponse;

        }

        //Url: api/accounts/validate
        [HttpGet(template: "validate")]
        public async Task<ActionResult<UserModel>> Validate()
        {
            var user = HttpContext.Items["User"] as UserModel;
            if(user is null)
            {
                return Unauthorized("You are not authorized to access this application");
            }
            return user;
        }
    }
}
