using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserCabinet.Domain.Configuration;
using UserCabinet.Domain.Entities.Users;
using UserCabinet.Service.DTOs.Users;
using UserCabinet.Service.Interfaces;

namespace UserCabinet.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService productService)
        {
            _userService = productService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<User>> AddAsync([FromForm]UserForCreationDTO dto)
            => Ok(await _userService.AddAsync(dto));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.GetAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _userService.GetAsync(p => p.Id == id));


        [HttpPut("{Id}")]
        public async ValueTask<ActionResult<User>> UpdateAsync([FromRoute(Name = "Id")] long id, [FromForm]UserForCreationDTO dto)
            => Ok(await _userService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _userService.DeleteAsync(p => p.Id == id));
    }
}