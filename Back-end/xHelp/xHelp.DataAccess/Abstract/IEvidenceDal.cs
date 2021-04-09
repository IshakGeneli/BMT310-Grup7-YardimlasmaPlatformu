using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Abstract
{
    public interface IEvidenceDal : IEntityRepository<Evidence>
    {
        Task AddEvidencesAsync(ICollection<Evidence> evidences);
        Task UpdateEvidencesAsync(ICollection<Evidence> evidences);
        Task AddEvidenceWithImageAsync(EvidenceImage evidenceImage);
        Task<Evidence> GetEvidenceWithImageAsync(Expression<Func<Evidence, bool>> filter = null);
        Task UpdateEvidenceWithImageAsync(Evidence evidence, Image image);
    }
}
