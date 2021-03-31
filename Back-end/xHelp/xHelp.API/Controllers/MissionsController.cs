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
    public class MissionsController : ControllerBase
    {
        private IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet("getMissions")]
        public async Task<IActionResult> GetMissions()
        {
            var result = await _missionService.GetAllAsync();
            return StatusCode(result.HttpStatusCode,result.Data);
        }

        [HttpPost("createMission")]
        public async Task<IActionResult> CreateMission([FromBody] CreateMissionDTO createMissionDTO)
        {
            var result = await _missionService.AddMissionAsync(createMissionDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpGet("getMissionById/{id}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var result = await _missionService.GetMissionByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }
    }
}
