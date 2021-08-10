namespace Affiliates.Shared.Models
{
	public class CreateUserModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}

	public class EditUserModel : CreateUserModel
	{
		public string Id { get; set; }
	}

	public class UserModel : EditUserModel
	{
		public bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(Id);
			}
		}
	}
}
