using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Abstract
{
    public interface IAchievementService
    {
        Task<ICollection<Achievement>> GetAllAsync();
        Task<Achievement> GetAchievementByIdAsync(int id);
        Task<Achievement> AddAchievementAsync(Achievement achievement);
        Task UpdateAchievementAsync(Achievement achievement);
        Task DeleteAchievementAsync(int id);
    }
}
