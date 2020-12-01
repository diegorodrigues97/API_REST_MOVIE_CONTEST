using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieContest.Domain.DTO.User;
using MovieContest.Domain.Services;
using System.Collections.Generic;

namespace MovieContest.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (!_userService.IsAdmin(User.Identity.Name))
                return Unauthorized();

            return Ok(_userService.GetById(id));
        }

          
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page)
        {
            if (!_userService.IsAdmin(User.Identity.Name))
                return Unauthorized();

            UserFilterDTO filter = new UserFilterDTO();
            filter.page = page;

            return Ok(_userService.Get(filter));
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if (!_userService.IsAdmin(User.Identity.Name))
                return Unauthorized();

            if (user == null)
                return BadRequest();

            return Ok(_userService.Add(user));
        }        

        [HttpPut]
        public IActionResult Put([FromBody] UserDTO user)
        {
            if (!_userService.IsAdmin(User.Identity.Name))
                return Unauthorized();

            if (user == null)
                return BadRequest();
            
            return Ok(_userService.Edit(user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!_userService.IsAdmin(User.Identity.Name))
                return Unauthorized(); 

            _userService.Delete(id);
            return NoContent();
        }
    }
}