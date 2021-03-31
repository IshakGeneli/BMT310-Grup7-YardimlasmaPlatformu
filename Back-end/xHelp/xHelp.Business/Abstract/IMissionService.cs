using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Abstract
{
    public interface IMissionService
    {
        Task<IDataResult<ICollection<Mission>>> GetAllAsync();
        Task<IDataResult<Mission>> GetMissionByIdAsync(int id);
        Task<IDataResult<Mission>> AddMissionAsync(CreateMissionDTO createMissionDTO);
        Task UpdateMissionAsync(Mission mission);
        Task DeleteMissionAsync(int id);
    }
}
