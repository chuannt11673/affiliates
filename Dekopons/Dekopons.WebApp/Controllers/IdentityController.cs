using Affiliates.Application.Services;
using Affiliates.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Affiliates.WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly IUserService _userService;

		public IdentityController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost, Route("users")]
		public async Task<UserModel> Create([FromBody] CreateUserModel model)
		{
			var result = await _userService.CreateAsync(model);
			return result;
		}

		[Authorize, HttpGet, Route("users/current")]
		public async Task<UserModel> GetCurrentUser()
		{
			var result = await _userService.GetUserInfoAsync();
			return result;
		}

	}
}
