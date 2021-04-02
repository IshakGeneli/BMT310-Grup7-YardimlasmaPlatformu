using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Abstract
{
    public interface IAchievementService
    {
        Task<ICollection<Achievement>> GetAllAsync();
        Task<IDataResult<Achievement>> GetAchievementByIdAsync(int id);
        Task<IDataResult<ICollection<Achievement>>> GetAllByUserIdAsync(string id);
        Task<IDataResult<Achievement>> AddAchievementAsync(CreateAchievementDTO createAchievementDTO);
        Task<IDataResult<Achievement>> UpdateAchievementAsync(UpdateAchievementDTO updateAchievementDTO);
        Task<IResult> DeleteAchievementAsync(int id);
    }
}
