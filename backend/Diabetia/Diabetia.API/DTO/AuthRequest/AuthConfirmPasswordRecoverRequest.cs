﻿namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthConfirmPasswordRecoverRequest
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
        public string Password { get; set; }
    }
}
