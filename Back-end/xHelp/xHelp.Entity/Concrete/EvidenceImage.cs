using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class EvidenceImage : IEntity
    {
        public int EvidenceId { get; set; }
        public Evidence Evidence { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
