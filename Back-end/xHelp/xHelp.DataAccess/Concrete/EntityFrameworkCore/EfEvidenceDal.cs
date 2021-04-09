using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess.EntityFrameworkCore;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfEvidenceDal : EfEntityRepositoryBase<Evidence, xHelpDbContext>, IEvidenceDal
    {
        public async Task AddEvidencesAsync(ICollection<Evidence> evidences)
        {
            using (var context = new xHelpDbContext())
            {
                await context.AddRangeAsync(evidences);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateEvidencesAsync(ICollection<Evidence> evidences)
        {
            using (var context = new xHelpDbContext())
            {
                context.UpdateRange(evidences);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddEvidenceWithImageAsync(EvidenceImage evidenceImage)
        {
            using (var context = new xHelpDbContext())
            {
                await context.EvidenceImages.AddAsync(evidenceImage);
                await context.SaveChangesAsync();
            }
        }
    }
}
