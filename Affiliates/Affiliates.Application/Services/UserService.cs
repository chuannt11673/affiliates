using Affiliates.Infratructure;
using Affiliates.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Affiliates.Application.Services
{
	public interface IUserService
	{
		Task<UserModel> CreateAsync(CreateUserModel model);
	}
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public UserService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<UserModel> CreateAsync(CreateUserModel model)
		{
			var existingUser = await _userManager.FindByNameAsync(model.Username);
			if (existingUser != null)
			{
				throw new Exception("Username already exists");
			}

			var user = new ApplicationUser
			{
				UserName = model.Username,
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				throw new Exception(result.Errors.Select(_ => _.Description).FirstOrDefault());
			}

			return new UserModel
			{
				Id = user.Id,
				Username = model.Username,
				Email = model.Email
			};
		}
	}
}
