using AuthorizationServiceDevelopment.Models.Users;
using AuthorizationServiceDevelopment.Services;
using AuthorizationServiceDevelopment.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServiceDevelopment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly Authenticator authenticator;

		public AuthorizationController(IUserService userService, Authenticator authenticator)
		{
			this.userService = userService;
			this.authenticator = authenticator;
		}

		[HttpPut("register")]
		public IActionResult Register(UserCreationModel creationModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			if (userService.CheckUserExistance(creationModel))
			{
				return Conflict("User already exist");
			}

			UserModel userModel = userService.CreateUser(creationModel);
			if (userModel == null)
			{
				return Unauthorized();
			}

			return Ok(authenticator.Authenticate(userModel));
		}

		[HttpPost("login")]
		public IActionResult Login(UserAuthenticationModel authorizationModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			UserModel userModel = this.userService.GetUser(authorizationModel);
			if (userModel == null)
			{
				return Unauthorized();
			}

			return Ok(authenticator.Authenticate(userModel));
		}

		[HttpGet("user/{userId:int}")]
		public IActionResult GetUserById(int userId)
		{
			UserModel userModel = this.userService.GetUserById(userId);
			if (userModel == null)
			{
				return NotFound("User not found");
			}

			return Ok(userModel);
		}

		[HttpPost("refresh")]
		public IActionResult Refresh()
		{
			throw new NotImplementedException();
		}
	}
}
