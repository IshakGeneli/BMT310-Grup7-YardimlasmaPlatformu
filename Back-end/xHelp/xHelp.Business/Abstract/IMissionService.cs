using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Abstract
{
    public interface IMissionService
    {
        Task<IDataResult<ICollection<Mission>>> GetAllAsync();
        Task<Mission> GetMissionByIdAsync(int id);
        Task<Mission> AddMissionAsync(Mission mission);
        Task UpdateMissionAsync(Mission mission);
        Task DeleteMissionAsync(int id);
    }
}
