using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieContest.Domain.DTO.Token;
using MovieContest.Domain.DTO.User;
using MovieContest.Domain.Services;

namespace MovieContest.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest("Usuário não informado!");

            user.type = Domain.Model.Enumeration.EUserType.DEFAULT;

            return Ok(_userService.Add(user));
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest("Ivalid client request");

            var token = _userService.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenDTO tokenDTO)
        {
            if (tokenDTO is null)
                return BadRequest("Ivalid client request");

            var token = _userService.RefreshToken(tokenDTO);
            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var result = _userService.RevokeToken(User.Identity.Name);

            if (!result)
                return BadRequest("Ivalid client request");

            return NoContent();
        }

    }
}