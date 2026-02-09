using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Auth.Requests;
using GasAwareness.API.Models.Auth.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace GasAwareness.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto requestDto)
        {
            var user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new UserLoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Username = user.UserName,
                    IsSuccess = true,
                    Roles = userRoles.ToArray()
                };
            }

            return new UserLoginResponseDto
            {
                IsSuccess = false,
                ResponseMessage = "Email veya şifre hatalı!"
            };
        }

        public async Task<bool> RegisterUserAsync(string roleName, UserRegisterRequestDto requestDto)
        {
            if (requestDto.Password != requestDto.RePassword) return false;

            var user = new User
            {
                UserName = requestDto.Email,
                Email = requestDto.Email
            };

            var result = await _userManager.CreateAsync(user, requestDto.Password);

            if (!result.Succeeded) return false;

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);

            return roleResult.Succeeded;
        }
    }
}