﻿using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
