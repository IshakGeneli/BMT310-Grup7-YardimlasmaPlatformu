using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Core.Utilities.Results.Concrete;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserManager(UserManager<User> userManager,
            RoleManager<UserRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IResult> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, userLoginDTO.Password, false, false);
            if (signInResult.Succeeded)
            {
                return new SuccessfulResult(HttpStatusCode.OK);
            }
           
            return new ErrorResult(HttpStatusCode.Unauthorized);
        }

        public async Task<IResult> Register(UserRegisterDTO userRegisterDTO)
        {

            User newUser = new User
            {
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.Email
            };
            IdentityResult userResult = await _userManager.CreateAsync(newUser, userRegisterDTO.Password);

            if (userResult.Succeeded)
            {
                bool isRoleExisting = await _roleManager.RoleExistsAsync("Admin");
                if (!isRoleExisting)
                {
                    UserRole newRole = new UserRole
                    {
                        Name = "Admin"
                    };
                    IdentityResult roleResult = await _roleManager.CreateAsync(newRole);

                    if (!roleResult.Succeeded)
                    {
                        return new ErrorResult(HttpStatusCode.InternalServerError);
                    }
                }

                _userManager.AddToRoleAsync(newUser, "Admin").Wait();
                return new SuccessfulResult(HttpStatusCode.Created);
            }
            else
            {
                return new ErrorResult(HttpStatusCode.Conflict);
            }
        }
    }
}
