using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Auth.Requests;
using GasAwareness.API.Models.Auth.Responses;

namespace GasAwareness.API.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(string roleName, UserRegisterRequestDto requestDto);
        Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto requestDto);
    }
}