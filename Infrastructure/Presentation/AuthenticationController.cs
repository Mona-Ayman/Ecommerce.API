
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
        {
            var result = await serviceManager.AuthenticationService.Login(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await serviceManager.AuthenticationService.Register(registerDTO);
            return Ok(result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist()
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.CheckEmailExist(user));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDTO>> GetUserByEmail()
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.GetUserAddress(user));
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.GetUserAddress(user));
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.UpdateUserAddress(address, user));
        }

    }
}
