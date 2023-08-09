using AuctionApi.Models;
using AuctionApi.Services;
using AuctionApi.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApi.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService= accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _accountService.GetJwt(dto);
            return Ok(token);
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            var usersDto = _accountService.GetAll();
            return Ok(usersDto);
        }
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById([FromRoute]int id)
        {
            var userDto = _accountService.GetById(id);
            return Ok(userDto);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateById([FromRoute]int id, [FromBody]UpdateUserDto dto)
        {
            _accountService.UpdateUser(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute]int id)
        {
            _accountService.DeleteUser(id);
            return NoContent();
        }
    }
}
