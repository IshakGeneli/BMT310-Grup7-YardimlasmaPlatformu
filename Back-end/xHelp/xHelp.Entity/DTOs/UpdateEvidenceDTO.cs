using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.DTOs
{
    public class UpdateEvidenceDTO
    {
        public int Id { get; set; }
        public string Argument { get; set; }

        public int MissionId { get; set; }
    }
}
