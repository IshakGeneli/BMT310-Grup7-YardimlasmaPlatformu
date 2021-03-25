using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.Concrete
{
    public class Achievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Score { get; set; }
    }
}
