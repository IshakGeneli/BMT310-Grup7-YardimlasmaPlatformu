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
                    return await context.Set<Mission>().Include(m => m.Evidences).Include(m => m.User).ToListAsync();
                return await context.Set<Mission>().Include(m => m.Evidences).Include(m => m.User).Where(filter).ToListAsync();
            }
        }

        public async Task<Mission> GetWithEvidencesAsync(Expression<Func<Mission, bool>> filter = null)
        {
            using (var context = new xHelpDbContext())
            {
                return await context.Set<Mission>().Include(m => m.Evidences).Include(m => m.User).SingleOrDefaultAsync(filter);
            }
        }

        public async Task AddMissionWithImageAsync(MissionImage missionImage)
        {
            using (var context = new xHelpDbContext())
            {
                await context.MissionImages.AddAsync(missionImage);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateMissionWithImageAsync(Mission mission, Image image)
        {
            await UpdateAsync(mission);
            using (var context = new xHelpDbContext())
            {
                context.Images.Update(image);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Mission> GetMissionWithImagesAsync(Expression<Func<Mission, bool>> filter = null)
        {
            using (var context = new xHelpDbContext())
            {
                return await context.Set<Mission>().Include(m => m.MissionImages).ThenInclude(mI => mI.Image).SingleOrDefaultAsync(filter);
            }
        }
    }
}
