using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class Evidence : IEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Argument { get; set; }
    }
}
