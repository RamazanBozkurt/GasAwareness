using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Requests;
using GasAwareness.API.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        /// <summary>
        /// Get survey detail by id
        /// </summary>
        /// <param name="id">Survey Id</param>
        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetSurveyAsync([FromQuery] Guid id)
        {
            var response = await _surveyService.GetSurveyAsync(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Get all surveys
        /// </summary>
        [HttpGet("main")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetSurveysAsync()
        {
            var response = await _surveyService.GetSurveyMainListAsync();

            return Ok(response);
        }

        /// <summary>
        /// Get all surveys
        /// </summary>
        [HttpGet("main/all")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetAllSurveysAsync()
        {
            var response = await _surveyService.GetAllSurveysMainListAsync();

            return Ok(response);
        }

        /// <summary>
        /// Create a new survey
        /// </summary>
        /// <param name="request">Survey Create Request</param>
        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateSurveyAsync([FromBody] CreateSurveyRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Title))
                return BadRequest("Anket başlığı boş olamaz.");

            if (request.Questions == null || !request.Questions.Any())
                return BadRequest("Anketin en az bir sorusu olmalıdır.");

            try
            {
                var resultId = await _surveyService.CreateSurveyAsync(request);

                return Ok(resultId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed survey create: {ex.Message}");
            }
        }

        /// <summary>
        /// Submit and save survey
        /// </summary>
        /// <param name="request">Survey Submit Request</param>
        [HttpPost("submit")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> SubmitSurvey([FromBody] SurveySubmitRequestDto request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı kimliği doğrulanamadı.");

            if (request == null || !request.Answers.Any())
                return BadRequest("Hiçbir cevap gönderilmedi.");

            try
            {
                var result = await _surveyService.SubmitSurveyAnswersAsync(userId, request);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to save survey: " + ex.Message);
            }
        }

        /// <summary>
        /// Get all users submitted surveys
        /// </summary>
        [HttpGet("userSurveys")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> GetUserSurveysAsync()
        {
            var response = await _surveyService.GetAllUserSurveysAsync();

            return Ok(response);
        }

        /// <summary>
        /// Get survey detail by submitted survey
        /// </summary>
        /// <param name="resultId">Survey Result Id</param>
        [HttpGet("userSurveys/detail")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> GetUserSurveyDetailAsync([FromQuery] Guid resultId)
        {
            var response = await _surveyService.GetUserSurveyDetailAsync(resultId);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}