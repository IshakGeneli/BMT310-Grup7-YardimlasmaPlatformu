using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.Concrete
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
    }
}
