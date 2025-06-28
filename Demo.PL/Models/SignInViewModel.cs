﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class SignInViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[MaxLength(6)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
