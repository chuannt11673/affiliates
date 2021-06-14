using Affiliates.Infratructure;
using Affiliates.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Affiliates.Application.Services
{
	public interface IUserService
	{
		Task<UserModel> CreateAsync(CreateUserModel model);
		Task<UserModel> GetUserInfoAsync();
	}
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;

		public CancellationToken CancellationToken { get; set; }

		public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;

			CancellationToken = _httpContextAccessor.HttpContext.RequestAborted;
		}

		public async Task<UserModel> CreateAsync(CreateUserModel model)
		{
			var existingUser = await _userManager.FindByNameAsync(model.Username);
			if (existingUser != null)
			{
				throw new Exception("Username already exists");
			}

			var user = _mapper.Map<ApplicationUser>(model);

			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				throw new Exception(result.Errors.Select(_ => _.Description).FirstOrDefault());
			}

			return _mapper.Map<UserModel>(user);
		}

		public async Task<UserModel> GetUserInfoAsync()
		{
			var context = _httpContextAccessor.HttpContext;
			var currentUser = context.User;

			if (!currentUser.Identity.IsAuthenticated)
			{
				throw new Exception("User isn't authenticated");
			}

			var userId = currentUser.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			var user = await _userManager.FindByIdAsync(userId);
			
			if (user == null)
			{
				throw new Exception("User is not found");
			}

			CancellationToken.ThrowIfCancellationRequested();
			return _mapper.Map<UserModel>(user);
		}
	}
}
