using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xHelp.Core.Entity;

namespace xHelp.Entity.Concrete
{
    public class User : IdentityUser, IEntity
    {
        public User()
        {
            Missions = new List<Mission>();
            Achievements = new List<Achievement>();
            UserImages = new List<UserImage>();
        }

        public string PublicId { get; set; }

        public Contact Contact { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
        public ICollection<UserImage> UserImages { get; set; }
    }
}
