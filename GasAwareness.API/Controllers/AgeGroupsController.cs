using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgeGroupsController : ControllerBase
    {
        private readonly IAgeGroupService _ageGroupService;
        public AgeGroupsController(IAgeGroupService ageGroupService)
        {
            _ageGroupService = ageGroupService;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetAgeGroupsAsync()
        {
            return Ok(await _ageGroupService.GetAgeGroupsAsync());
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> CreateAgeGroupAsync(CreateAgeGroupRequestDto request)
        {
            var response = await _ageGroupService.CreateAgeGroupAsync(request);

            if (!response) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> DeleteAgeGroupAsync([FromQuery] Guid id)
        {
            var response = await _ageGroupService.DeleteAgeGroupAsync(id);

            if (!response) return BadRequest();

            return Ok();
        }
    }
}