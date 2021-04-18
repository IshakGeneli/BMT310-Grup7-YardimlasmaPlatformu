using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Utilities;
using xHelp.Business.Utilities.Abstract;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Core.Utilities.Results.Concrete;
using xHelp.DataAccess.Abstract;
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
        private readonly IUserDal _userDal;
        private ICloudinaryOperations _cloudinaryOperations;

        public UserManager(UserManager<User> userManager,
            RoleManager<UserRole> roleManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IUserDal userDal,
            ICloudinaryOperations cloudinaryOperations)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userDal = userDal;
            _cloudinaryOperations = cloudinaryOperations;
        }

        public async Task<IDataResult<List<User>>> GetAllUserInformationsAsync()
        {
            var usersWithInformations = await _userDal.GetListAsync();

            return new SuccessfulDataResult<List<User>>(usersWithInformations, HttpStatusCode.OK);
        }

        public async Task<IDataResult<User>> GetUserByIdAsync(string id)
        {
            var user = await _userDal.GetWithImageAsync(u => u.Id == id);

            return new SuccessfulDataResult<User>(user, HttpStatusCode.OK);
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

                return new SuccessfulDataResult<String>(tokenString, HttpStatusCode.OK);
            }

            return new ErrorDataResult<String>("", HttpStatusCode.Unauthorized);
        }

        public async Task<IResult> Register(UserRegisterDTO userRegisterDTO)
        {
            User newUser = new User
            {
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.Email,
                NormalizedEmail = userRegisterDTO.Email
            };

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

            var uploadResult = await _cloudinaryOperations.UploadImageAsync(userRegisterDTO.ImageFile);

            await AddUserWithImageAsync(newUser, uploadResult);

            var user = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, userRegisterDTO.Password);

            _userManager.AddToRoleAsync(user, "Admin").Wait();
            return new SuccessfulResult(HttpStatusCode.Created);
        }

        private async Task AddUserWithImageAsync(User user, ImageUploadResult imageUploadResult)
        {
            user.PublicId = imageUploadResult.PublicId;
            var userImage = new UserImage
            {
                Image = new Image
                {
                    Url = imageUploadResult.Url.ToString()
                },
                User = user
            };
            await _userDal.AddUserWithImageAsync(userImage);
        }
    }
}

