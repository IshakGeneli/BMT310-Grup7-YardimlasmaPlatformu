
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Concrete;
using xHelp.Business.Utilities.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IUserDal> _mockUserDal;
        private Mock<ICloudinaryOperations> _mockCloudinaryOperations;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<RoleManager<UserRole>> _mockRoleManager;
        private Mock<SignInManager<User>> _mockSignInManager;

        private List<User> _users;

        [TestInitialize]
        public void Start()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserDal = new Mock<IUserDal>();
            _mockCloudinaryOperations = new Mock<ICloudinaryOperations>();
            _mockUserManager = new Mock<UserManager<User>>();
            _mockRoleManager = new Mock<RoleManager<UserRole>>();
            _mockSignInManager = new Mock<SignInManager<User>>();

            _users = new List<User>
            {
                new User {Id="1", Email="example1@gmail.com", PublicId="qwe", UserName="example1"},
                new User {Id="2", Email="example2@gmail.com", PublicId="qwe", UserName="example2"},
                new User {Id="3", Email="example3@gmail.com", PublicId="qwe", UserName="example3"},
                new User {Id="4", Email="example4@gmail.com", PublicId="qwe", UserName="example4"},
                new User {Id="5", Email="example5@gmail.com", PublicId="qwe", UserName="example5"},
            };
            _mockUserDal.Setup(m => m.GetListAsync(null).Result).Returns(_users);
        }
        /*
        [TestMethod]
        public async Task GetAllUserInformationsAsync()
        {
            // Arrange
            IUserService _userService = new UserManager(_mockUserManager.Object, _mockRoleManager.Object, _mockSignInManager.Object, _mockConfiguration.Object, _mockUserDal.Object, _mockCloudinaryOperations.Object);
            // Act
            List<User> users = (await _userService.GetAllUserInformationsAsync()).Data;
            // Assert
            Assert.AreEqual(5, users.Count);
        }*/
    }
}
