using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Auth.Responses
{
    public class UserLoginResponseDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string[] Roles { get; set; }
    }
}