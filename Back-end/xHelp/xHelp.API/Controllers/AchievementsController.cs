using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Entity.DTOs;

namespace xHelp.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : Controller
    {
        private IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("getAllByUserId/{id}")]
        public async Task<IActionResult> GetAllByUserId(string id)
        {
            var result = await _achievementService.GetAllByUserIdAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpGet("getAchievementById/{id}")]
        public async Task<IActionResult> GetAchievementById(int id)
        {
            var result = await _achievementService.GetAchievementByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpPost("createAchievement")]
        public async Task<IActionResult> CreateAchievement([FromBody] CreateAchievementDTO createAchievementDTO)
        {
            var result = await _achievementService.AddAchievementAsync(createAchievementDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpPut("updateAchievement")]
        public async Task<IActionResult> UpdateAchievement([FromBody] UpdateAchievementDTO updateAchievementDTO)
        {
            var result = await _achievementService.UpdateAchievementAsync(updateAchievementDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpDelete("deleteAchievement/{id}")]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            var result = await _achievementService.DeleteAchievementAsync(id);
            return StatusCode(result.HttpStatusCode);
        }
    }
}
