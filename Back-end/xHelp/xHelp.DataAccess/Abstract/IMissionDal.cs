using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Abstract
{
    public interface IMissionDal : IEntityRepository<Mission>
    {
        Task<ICollection<Mission>> GetListWithEvidencesAsync(Expression<Func<Mission, bool>> filter = null);
        Task<Mission> GetWithEvidencesAsync(Expression<Func<Mission, bool>> filter = null);
    }
}
