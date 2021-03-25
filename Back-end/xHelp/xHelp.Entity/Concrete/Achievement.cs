using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class Achievement : IEntity
    {
        public int Id { get; set; }
        public string Score { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
