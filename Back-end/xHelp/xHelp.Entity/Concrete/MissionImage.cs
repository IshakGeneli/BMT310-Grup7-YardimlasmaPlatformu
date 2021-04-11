using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class MissionImage : IEntity
    {
        public int MissionId { get; set; }
        public Mission Mission { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
