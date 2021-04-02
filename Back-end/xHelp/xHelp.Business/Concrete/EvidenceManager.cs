using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Concrete
{
    public class EvidenceManager : IEvidenceService
    {
        private readonly IEvidenceDal _evidenceDal;

        public EvidenceManager(IEvidenceDal evidenceDal)
        {
            _evidenceDal = evidenceDal;
        }

        public async Task<Evidence> AddEvidenceAsync(Evidence evidence)
        {
            return await _evidenceDal.AddAsync(evidence);
        }

        public async Task AddEvidencesAsync(ICollection<Evidence> evidences)
        {
            await _evidenceDal.AddEvidencesAsync(evidences);
        }

        public async Task DeleteEvidenceAsync(int id)
        {
            await _evidenceDal.DeleteAsync(new Evidence { Id = id });
        }

        public async Task<ICollection<Evidence>> GetAllAsync()
        {
            return await _evidenceDal.GetListAsync();
        }

        public async Task<Evidence> GetEvidenceByIdAsync(int id)
        {
            return await _evidenceDal.GetAsync(m => m.Id == id);
        }

        public async Task UpdateEvidenceAsync(Evidence evidence)
        {
            await _evidenceDal.UpdateAsync(evidence);
        }

        public async Task UpdateEvidencesAsync(ICollection<Evidence> evidences)
        {
            await _evidenceDal.UpdateEvidencesAsync(evidences);
        }
    }
}
