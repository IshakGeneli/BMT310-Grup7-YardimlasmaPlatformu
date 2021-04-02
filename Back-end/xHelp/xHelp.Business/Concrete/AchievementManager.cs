using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Core.Utilities.Results.Concrete;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Concrete
{
    public class AchievementManager : IAchievementService
    {
        private readonly IAchievementDal _achievementDal;
        private readonly IMapper _mapper;

        public AchievementManager(IAchievementDal achievementDal, IMapper mapper)
        {
            _achievementDal = achievementDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<Achievement>> AddAchievementAsync(CreateAchievementDTO createAchievementDTO)
        {
            var achievement = _mapper.Map<Achievement>(createAchievementDTO);
            var newAchievement = await _achievementDal.AddAsync(achievement);

            return new SuccessfulDataResult<Achievement>(newAchievement, HttpStatusCode.Created);
        }

        public async Task<IResult> DeleteAchievementAsync(int id)
        {
            await _achievementDal.DeleteAsync(new Achievement { Id = id });

            return new SuccessfulResult(HttpStatusCode.OK);
        }

        public async Task<ICollection<Achievement>> GetAllAsync()
        {
            return await _achievementDal.GetListAsync();
        }

        public async Task<IDataResult<ICollection<Achievement>>> GetAllByUserIdAsync(string id)
        {
            var achievements = await _achievementDal.GetListAsync(a => a.UserId == id);

            return new SuccessfulDataResult<ICollection<Achievement>>(achievements,HttpStatusCode.OK);
        }

        public async Task<IDataResult<Achievement>> GetAchievementByIdAsync(int id)
        {
            var achievement = await _achievementDal.GetAsync(a => a.Id == id);

            return new SuccessfulDataResult<Achievement>(achievement, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Achievement>> UpdateAchievementAsync(UpdateAchievementDTO updateAchievementDTO)
        {
            var achievement = _mapper.Map<Achievement>(updateAchievementDTO);
            var updatedAchievement = await _achievementDal.UpdateAsync(achievement);

            return new SuccessfulDataResult<Achievement>(updatedAchievement, HttpStatusCode.OK);
        }
    }
}
