using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.SubscriptionType.Requests;
using GasAwareness.API.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionTypesController : ControllerBase
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        public SubscriptionTypesController(ISubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionTypeService = subscriptionTypeService;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetSubscriptionTypesAsync()
        {
            return Ok(await _subscriptionTypeService.GetSubscriptionTypesAsync());
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> CreateSubscriptionTypeAsync(CreateSubscriptionTypeRequestDto request)
        {
            var response = await _subscriptionTypeService.CreateSubscriptionTypeAsync(request);

            if (!response) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> DeleteSubscriptionTypeAsync([FromQuery] Guid id)
        {
            var response = await _subscriptionTypeService.DeleteSubscriptionTypeAsync(id);

            if (!response) return BadRequest();

            return Ok();
        }
    }
}