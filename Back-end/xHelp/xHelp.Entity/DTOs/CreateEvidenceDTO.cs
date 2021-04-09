using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.DTOs
{
    public class CreateEvidenceDTO
    {
        public String Argument { get; set; }
        public IFormFile ImageFile { get; set; }
        public int MissionId { get; set; }
    }
}
