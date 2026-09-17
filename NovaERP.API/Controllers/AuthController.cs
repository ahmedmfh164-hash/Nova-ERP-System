using BCrypt.Net;
using ERP.Application.Interfaces.Servicies;
using ERP.Application.Servicies;
using ERP.Contacts.Requests.Auth;
using ERP.Contacts.Responses.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
       public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService=authService;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO request)
        {
            var loginResult = await _authService.LoginAsync(request);
            if (loginResult.Status == TokenResponseDTO.ResultStatus.InvalidCredentials)
            {
                return Unauthorized(loginResult.Message);
            }

            if (loginResult.Status == TokenResponseDTO.ResultStatus.InactiveAccount)
            {
                return Unauthorized(loginResult.Message);
            }

            if (loginResult.Status == TokenResponseDTO.ResultStatus.AlreadyLoggedIn)
            {
                return Ok(loginResult.Message);
            }

            return Ok(new TokenResponseDTO
            {
                AccessToken = loginResult.AccessToken,
                RefreshToken = loginResult.RefreshToken  ,
                Status = loginResult.Status,
                Message = loginResult.Message
            });

        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO request)
        {
             var refreshResult=await _authService.RefreshAsync(request);

            if (refreshResult.Status == TokenResponseDTO.ResultStatus.InvalidCredentials)
            {
                return Unauthorized(refreshResult.Message);
            }

            if (refreshResult.Status == TokenResponseDTO.ResultStatus.Expired)
            {
                return Unauthorized(refreshResult.Message);
            }

            if (refreshResult.Status == TokenResponseDTO.ResultStatus.Revoked)
            {
                return Unauthorized(refreshResult.Message);
            }

            if (refreshResult.Status == TokenResponseDTO.ResultStatus.Invalid)
            {
                return Unauthorized(refreshResult.Message);
            }

            return Ok(new TokenResponseDTO
            {
                AccessToken = refreshResult.AccessToken,
                RefreshToken = refreshResult.RefreshToken,
                Status = refreshResult.Status,
                Message = refreshResult.Message
            });
        }

        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDTO request)
        {
            var logoutResult = await _authService.LogoutAsync(request);

            if (logoutResult.Status == TokenResponseDTO.ResultStatus.InvalidCredentials)
                return Ok();

            if (logoutResult.Status == TokenResponseDTO.ResultStatus.Invalid)
                return Ok();

            if (logoutResult.Status == TokenResponseDTO.ResultStatus.Revoked)
            return Ok("Logged out successfully");

            return Unauthorized("Fail");
        }

    }
}
