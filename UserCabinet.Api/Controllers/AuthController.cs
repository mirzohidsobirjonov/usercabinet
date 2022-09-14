using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserCabinet.Service.DTOs.Users;
using UserCabinet.Service.Interfaces;

namespace UserCabinet.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDTO dto)
        {
            var token = await authService.GenerateTokenAsync(dto.Username, dto.Password);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
