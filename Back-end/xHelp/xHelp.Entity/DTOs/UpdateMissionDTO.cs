using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.Entity.DTOs
{
    public class UpdateMissionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedDate { get; set; }

        public string OwnerUserId { get; set; }
        public ICollection<UpdateEvidenceDTO> UpdateEvidenceDTOs { get; set; }
    }
}
