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
            MissionImages = new List<MissionImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Difficulty { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime CreatedDate { get; set; }

        public string OwnerUserId { get; set; }
        public User User { get; set; }
        public ICollection<Evidence> Evidences { get; set; }
        public ICollection<MissionImage> MissionImages { get; set; }
    }
}
