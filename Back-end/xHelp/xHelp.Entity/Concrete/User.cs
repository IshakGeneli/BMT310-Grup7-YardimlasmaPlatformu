using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xHelp.Entity.Concrete
{
    public class User : IdentityUser
    {
        public User()
        {
            Missions = new List<Mission>();
            Achievements = new List<Achievement>();
        }

        public Contact Contact { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
    }
}
