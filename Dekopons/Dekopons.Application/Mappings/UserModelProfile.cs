using Affiliates.Infratructure;
using Affiliates.Shared.Models;
using AutoMapper;

namespace Affiliates.Application.Mappings
{
	public class UserModelProfile :  Profile
	{
		public UserModelProfile()
		{
			CreateMap<ApplicationUser, UserModel>();
			CreateMap<CreateUserModel, ApplicationUser>();
		}
	}
}
