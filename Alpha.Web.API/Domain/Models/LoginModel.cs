﻿using System.ComponentModel.DataAnnotations;

namespace Alpha.Web.API.Domain.Models
{
    public class LoginModel
    {
        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

}
