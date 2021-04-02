using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Entity.DTOs;

namespace xHelp.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EvidencesController : Controller
    {
        private IEvidenceService _evidenceService;

        public EvidencesController(IEvidenceService evidenceService)
        {
            _evidenceService = evidenceService;
        }

        [HttpGet("getAllByMissionId/{id}")]
        public async Task<IActionResult> GetAllByMissionId(int id)
        {
            var result = await _evidenceService.GetAllByMissionIdAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpGet("getEvidenceById/{id}")]
        public async Task<IActionResult> GetEvidenceById(int id)
        {
            var result = await _evidenceService.GetEvidenceByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpPost("createEvidence")]
        public async Task<IActionResult> CreateEvidence([FromBody] CreateEvidenceDTO createEvidenceDTO)
        {
            var result = await _evidenceService.AddEvidenceAsync(createEvidenceDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpPut("updateEvidence")]
        public async Task<IActionResult> UpdateEvidence([FromBody] UpdateEvidenceDTO updateEvidenceDTO)
        {
            var result = await _evidenceService.UpdateEvidenceAsync(updateEvidenceDTO);
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        [HttpDelete("deleteEvidence/{id}")]
        public async Task<IActionResult> DeleteEvidence(int id)
        {
            var result = await _evidenceService.DeleteEvidenceAsync(id);
            return StatusCode(result.HttpStatusCode);
        }
    }
}
