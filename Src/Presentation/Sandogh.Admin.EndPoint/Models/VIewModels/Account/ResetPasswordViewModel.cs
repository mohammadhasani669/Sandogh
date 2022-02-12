﻿using System.ComponentModel.DataAnnotations;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string? UserName { get; set; }
    }
}
