using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess.EntityFrameworkCore;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfMissionDal : EfEntityRepositoryBase<Mission, xHelpDbContext>, IMissionDal
    {
        public async Task<ICollection<Mission>> GetListWithEvidencesAsync(Expression<Func<Mission, bool>> filter = null)
        {
            using (var context = new xHelpDbContext())
            {
                if (filter == null)
                    return await context.Set<Mission>().Include(m => m.Evidences).ToListAsync();
                return await context.Set<Mission>().Include(m => m.Evidences).Where(filter).ToListAsync();
            }
        }

        public async Task<Mission> GetWithEvidencesAsync(Expression<Func<Mission, bool>> filter = null)
        {
            using (var context = new xHelpDbContext())
            {
                return await context.Set<Mission>().Include(m => m.Evidences).SingleOrDefaultAsync(filter);
            }
        }
    }
}
