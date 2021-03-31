using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Business.Abstract;

namespace xHelp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getMissions")]
        public async Task<IActionResult> GetMissions()
        {
            var result = await _missionService.GetAllAsync();
            return StatusCode(result.HttpStatusCode,result.Data);
        }
    }
}
