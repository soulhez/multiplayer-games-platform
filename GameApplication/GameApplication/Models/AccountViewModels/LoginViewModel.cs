﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Podano niepoprawy adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password, ErrorMessage = "Niepoprawne hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać?")]
        public bool RememberMe { get; set; }
    }
}
