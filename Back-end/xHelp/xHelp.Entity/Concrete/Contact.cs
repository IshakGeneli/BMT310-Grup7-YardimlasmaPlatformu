using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
