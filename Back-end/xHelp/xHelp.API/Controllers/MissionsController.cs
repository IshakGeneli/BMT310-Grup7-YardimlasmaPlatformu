using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Entity.Concrete;
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

        [HttpGet("getAllWithEvidences")]
        public async Task<IActionResult> GetAllWithEvidences()
        {
            var result = await _missionService.GetAllWithEvidencesAsync();
            return StatusCode(result.HttpStatusCode,result.Data);
        }

        [HttpPost("createMission")]
        public async Task<IActionResult> CreateMission([FromForm] CreateMissionDTO createMissionDTO)
        {
            var result = await _missionService.AddMissionAsync(createMissionDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpGet("getMissionByIdWithEvidences/{id}")]
        public async Task<IActionResult> GetMissionByIdWithEvidences(int id)
        {
            var result = await _missionService.GetMissionByIdWithEvidencesAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpPost("createEvidenceOnMission")]
        public async Task<IActionResult> CreateEvidenceOnMission([FromForm] CreateEvidenceDTO createEvidenceDTO)
        {
            await _missionService.CreateEvidenceOnMission(createEvidenceDTO);
            return StatusCode(200);
        }

        /*
        [HttpPut("updateMissionWithEvidences")]
        public async Task<IActionResult> UpdateMissionWithEvidences([FromBody] UpdateMissionWithEvidencesDTO updateMissionWithEvidencesDTO)
        {
            var result = await _missionService.UpdateMissionWithEvidencesAsync(updateMissionWithEvidencesDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }*/

        [HttpPut("updateMission")]
        public async Task<IActionResult> UpdateMission([FromForm] UpdateMissionDTO updateMissionDTO)
        {
            var result = await _missionService.UpdateMissionAsync(updateMissionDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpDelete("deleteMission/{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var result = await _missionService.DeleteMissionAsync(id);
            return StatusCode(result.HttpStatusCode);
        }
    }
}
