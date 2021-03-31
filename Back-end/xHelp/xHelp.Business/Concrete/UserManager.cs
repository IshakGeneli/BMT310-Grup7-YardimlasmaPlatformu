using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;

        public UserManager(UserManager<User> userManager,
            RoleManager<UserRole> roleManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IDataResult<String>> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, userLoginDTO.Password, false, false);

            if (signInResult.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new SuccessfulDataResult<String>(tokenString,HttpStatusCode.OK);
            }
           
            return new ErrorDataResult<String>("",HttpStatusCode.Unauthorized);
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
