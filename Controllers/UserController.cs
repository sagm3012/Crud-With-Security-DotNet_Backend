using Auth1.Authorition;
using Auth1.Core.Entity;
using Auth1.Core.Interface;
using Auth1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Auth1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserService userService,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor= httpContextAccessor;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest user3)
        {
            var response = await _userService.Authenticate(user3);
            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(response);
        }

     /*   public IHttpContextAccessor Get_httpContextAccessor()
        {
            return _httpContextAccessor;
        }*/

        [HttpPost]
        public async Task<IActionResult> RefreshToken(IHttpContextAccessor _httpContextAccessor)
        {
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            return Ok(token);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAll2([FromBody] TableMetaData metaData)
        {
            var result = await _userService.GetAll2(metaData);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] ApplicationUserDTO model)
        {
            var result = await _userService.AddUser(model);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromBody] ApplicationUserDTO model, int id)
        {
            var result =  await _userService.UpdateUser(model, id);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var result = await _userService.RemoveUser(id);
            return Ok(result);
        }
    }
}
