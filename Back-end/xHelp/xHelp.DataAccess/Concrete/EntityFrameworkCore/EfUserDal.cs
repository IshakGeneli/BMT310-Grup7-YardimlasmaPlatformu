using System;
using System.Collections.Generic;
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
    }
}
