using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.Concrete
{
    public class Image
    {
        public Image()
        {
            EvidenceImages = new List<EvidenceImage>();
            MissionImages = new List<MissionImage>();
            UserImages = new List<UserImage>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }

        public ICollection<EvidenceImage> EvidenceImages { get; set; }
        public ICollection<MissionImage> MissionImages { get; set; }
        public ICollection<UserImage> UserImages { get; set; }
    }
}
