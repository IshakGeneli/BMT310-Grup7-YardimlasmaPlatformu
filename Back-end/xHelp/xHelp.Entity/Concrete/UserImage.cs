using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class UserImage : IEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
