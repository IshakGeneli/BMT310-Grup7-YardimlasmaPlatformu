using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.Concrete
{
    public class Task
    {
        public int Id { get; set; }
        public int OwnerUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
