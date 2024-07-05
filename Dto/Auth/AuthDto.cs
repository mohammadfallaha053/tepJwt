﻿using System;
using System.Collections.Generic;

namespace JWT53.Dto.Auth
{
    public class AuthDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string  PhoneNumber {  get; set; }

        public string ImageUrl { get; set; }
    }
}