using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Abstract
{
    public interface IEvidenceService
    {
        Task<ICollection<Evidence>> GetAllAsync();
        Task<IDataResult<ICollection<Evidence>>> GetAllByMissionIdAsync(int missionId);
        Task<IDataResult<Evidence>> GetEvidenceByIdAsync(int id);
        Task<IDataResult<Evidence>> AddEvidenceAsync(CreateEvidenceDTO createEvidenceDTO);
        Task AddEvidencesAsync(ICollection<Evidence> evidences);
        Task<IDataResult<Evidence>> UpdateEvidenceAsync(UpdateEvidenceDTO updateEvidenceDTO);
        Task UpdateEvidencesAsync(ICollection<Evidence> evidences);
        Task<IResult> DeleteEvidenceAsync(int id);
    }
}
