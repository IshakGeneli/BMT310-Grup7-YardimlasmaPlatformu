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
        Task<IDataResult<ICollection<Mission>>> GetAllWithEvidencesAsync();
        Task<IDataResult<Mission>> GetMissionByIdWithEvidencesAsync(int id);
        Task<IDataResult<Mission>> AddMissionAsync(CreateMissionDTO createMissionDTO);
        Task CreateEvidenceOnMission(CreateEvidenceDTO createEvidenceDTO);
        Task<IDataResult<Mission>> UpdateMissionWithEvidencesAsync(UpdateMissionWithEvidencesDTO updateMissionWithEvidencesDTO);
        Task<IDataResult<Mission>> UpdateMissionAsync(UpdateMissionDTO updateMissionDTO);
        Task<IResult> DeleteMissionAsync(int id);
    }
}
