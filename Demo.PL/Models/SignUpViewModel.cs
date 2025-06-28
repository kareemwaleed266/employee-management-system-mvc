﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        public string Password { get; set; }
        [Required]
        [MaxLength(6)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }

    }
}
