using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess.EntityFrameworkCore;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfUserDal : EfEntityRepositoryBase<User, xHelpDbContext>, IUserDal
    {
        public async Task AddUserWithImageAsync(UserImage userImage)
        {
            using (var context = new xHelpDbContext())
            {
                await context.UserImages.AddAsync(userImage);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User> GetWithImageAsync(Expression<Func<User, bool>> filter = null)
        {
            using (var context = new xHelpDbContext())
            {
                return await context.Set<User>().Include(m => m.UserImages).ThenInclude(uI => uI.Image).SingleOrDefaultAsync(filter);
            }
        }
    }
}
