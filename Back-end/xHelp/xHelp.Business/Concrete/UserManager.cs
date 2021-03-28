using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
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

        public async Task Register(UserRegisterDTO userRegisterDTO)
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
                }

                _userManager.AddToRoleAsync(newUser, "Admin").Wait();
            }
        }
    }
}
