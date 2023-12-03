using AuthorizationServiceDevelopment.Managers;
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
		private readonly IPasswordManager passwordManager;

		public AuthorizationController(IUserService userService, Authenticator authenticator, IPasswordManager passwordManager)
		{
			this.userService = userService;
			this.authenticator = authenticator;
			this.passwordManager = passwordManager;
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

			string passwordHash = passwordManager.GeneratePasswordHash(creationModel.Password, out string salt);
			UserModel userModel = userService.CreateUser(creationModel, passwordHash, salt);
			if (userModel == null)
			{
				return BadRequest("Fail to create a new user");
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
				return NotFound("Failed to login");
			}

			return Ok(authenticator.Authenticate(userModel));
		}

		[HttpGet("user/{userId:guid}")]
		public IActionResult GetUserById(Guid userId)
		{
			UserModel userModel = this.userService.GetUserById(userId);
			if (userModel == null)
			{
				return NotFound("User not found");
			}

			return Ok(userModel);
		}

		[HttpGet("user/{email}/salt")]
		public ActionResult GetUserSalt(string email)
		{
			string salt = this.userService.GetUserSalt(email);
			if (string.IsNullOrEmpty(salt))
			{
				return NotFound("User not found");
			}

			return Ok(salt);
		}

		[HttpPost("refresh")]
		public IActionResult Refresh()
		{
			throw new NotImplementedException();
		}
	}
}
