using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> Register(UserRegisterDTO userRegisterDTO);
        Task<IResult> Login(UserLoginDTO userLoginDTO);
    }
}
