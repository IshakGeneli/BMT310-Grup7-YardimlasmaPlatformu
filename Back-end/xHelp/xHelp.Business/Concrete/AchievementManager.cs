using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Concrete
{
    public class AchievementManager : IAchievementService
    {
        private readonly IAchievementDal _achievementDal;

        public AchievementManager(IAchievementDal achievementDal)
        {
            _achievementDal = achievementDal;
        }

        public async Task<Achievement> AddAchievementAsync(Achievement achievement)
        {
            return await _achievementDal.AddAsync(achievement);
        }

        public async Task DeleteAchievementAsync(int id)
        {
            await _achievementDal.DeleteAsync(new Achievement { Id = id });
        }

        public async Task<ICollection<Achievement>> GetAllAsync()
        {
            return await _achievementDal.GetListAsync();
        }

        public async Task<Achievement> GetAchievementByIdAsync(int id)
        {
            return await _achievementDal.GetAsync(m => m.Id == id);
        }

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            await _achievementDal.UpdateAsync(achievement);
        }
    }
}
