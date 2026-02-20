using LMSBackend.Application.Features.TokenRotation.Commands;
using LMSBackend.Application.Features.TokenRotation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMSBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-token")]
        public async Task<IActionResult> CreateToken([FromBody] CreateTokenCommand command)
        {
            try
            {
                var (accessToken, refreshToken) = await _mediator.Send(command);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);

                return Ok(new { AccessToken = accessToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("rotate-token")]
        public async Task<IActionResult> RotateToken([FromBody] RotateTokenCommand request)
        {
            try
            {
                var (newAccessToken, newRefreshToken) = await _mediator.Send(request);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("refresh_token", newRefreshToken, cookieOptions);
                
                return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("get-refresh-token")]
        public async Task<IActionResult> GetRefreshToken([FromQuery] Guid userId, [FromQuery] Guid deviceId)
        {
            try
            {
                var refreshToken = await _mediator.Send(new GetRefreshTokenQuery { UserId = userId, DeviceId = deviceId });
                return Ok(refreshToken);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("ping")]
        public async Task<IActionResult> Ping([FromBody] PingSessionCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok("Session is active");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}