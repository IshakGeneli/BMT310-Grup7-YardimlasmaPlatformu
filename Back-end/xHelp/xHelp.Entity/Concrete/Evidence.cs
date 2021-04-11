using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class Evidence : IEntity
    {
        public Evidence()
        {
            EvidenceImages = new List<EvidenceImage>();
        }

        public int Id { get; set; }
        public string Argument { get; set; }
        public string PublicId { get; set; }

        public int MissionId { get; set; }
        public Mission Mission { get; set; }

        public ICollection<EvidenceImage> EvidenceImages { get; set; }
    }
}
