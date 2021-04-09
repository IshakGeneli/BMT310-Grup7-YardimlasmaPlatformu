using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task AddUserWithImageAsync(UserImage userImage);
        Task<User> GetWithImageAsync(Expression<Func<User, bool>> filter = null);
    }
}
