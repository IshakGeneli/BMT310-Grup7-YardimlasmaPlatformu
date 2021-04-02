using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.DTOs
{
    public class CreateEvidenceListDTO
    {
        public ICollection<CreateEvidenceDTO> CreateEvidencesDTO { get; set; }
        public int MissionId { get; set; }
    }
}
