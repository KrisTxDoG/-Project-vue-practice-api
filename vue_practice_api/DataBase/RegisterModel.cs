﻿using Microsoft.AspNetCore.Identity;

namespace vue_practice_api.DataBase
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
