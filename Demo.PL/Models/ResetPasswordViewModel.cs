using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class ResetPasswordViewModel
	{
		public string Email { get; set; }
		public string Token { get; set; }
		[Required]
		[MaxLength(6)]
		public string Password { get; set; }

		[Required]
		[MaxLength(6)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
