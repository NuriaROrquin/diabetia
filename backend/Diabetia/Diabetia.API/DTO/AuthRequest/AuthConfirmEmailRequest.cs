﻿namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthConfirmEmailRequest : AuthUserRequest
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
