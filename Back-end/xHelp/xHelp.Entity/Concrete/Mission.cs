using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class Mission : IEntity
    {
        public Mission()
        {
            Evidences = new List<Evidence>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedDate { get; set; }

        public int OwnerUserId { get; set; }
        public User User { get; set; }
        public ICollection<Evidence> Evidences { get; set; }
    }
}
