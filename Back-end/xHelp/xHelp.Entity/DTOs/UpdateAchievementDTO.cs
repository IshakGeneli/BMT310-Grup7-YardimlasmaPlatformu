using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.DTOs
{
    public class UpdateAchievementDTO
    {
        public int Id { get; set; }
        public string Score { get; set; }

        public string UserId { get; set; }
    }
}
