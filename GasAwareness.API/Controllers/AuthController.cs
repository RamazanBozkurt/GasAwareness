using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Enums;
using GasAwareness.API.Models.Auth.Requests;
using GasAwareness.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Create a new user which role name is Visitor
        /// </summary>
        /// <param name="request">User Create Request</param>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterVisitor(UserRegisterRequestDto request)
        {
            bool response = await _authService.RegisterUserAsync(RoleName.Visitor, request);

            if (!response) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Create a new user which role name is Admin (Postman only) (For Test!)
        /// </summary>
        /// <param name="request">User Create Request</param>
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin(UserRegisterRequestDto request)
        {
            bool response = await _authService.RegisterUserAsync(RoleName.Admin, request);

            if (!response) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Create a new user which role name is Editor (Postman only) (For Test!)
        /// </summary>
        /// <param name="request">User Create Request</param>
        [HttpPost("registerEditor")]
        public async Task<IActionResult> RegisterEditor(UserRegisterRequestDto request)
        {
            bool response = await _authService.RegisterUserAsync(RoleName.Editor, request);

            if (!response) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="request">User Login Request</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto request)
        {
            var response = await _authService.LoginUserAsync(request);

            var test = User.IsInRole(RoleName.Admin);

            if (!response.IsSuccess) return BadRequest(response.ResponseMessage);

            return Ok(response);
        }
    }
}