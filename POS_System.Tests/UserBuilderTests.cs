using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_System.Auth;
using POS_System.Auth.Models;
using POS_System.Auth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS_System.Tests
{
    [TestClass]
    public class UserBuilderTests
    {
        private AuthDbContext _context;
        private UserBuilder _userBuilder;

        public UserBuilderTests()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AuthDbContext(options);
            _userBuilder = new UserBuilder(_context);
        }

        [TestInitialize]
        public void Setup()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task Build_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User"
            };
            _userBuilder.SetUsername(user.UserName)
                        .SetEmail(user.Email)
                        .SetFirstName(user.FirstName)
                        .SetLastName(user.LastName);

            // Act
            var result = await _userBuilder.Build();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserName, result.UserName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(1, await _context.Users.CountAsync());
        }

        [TestMethod]
        public async Task Build_ShoulReturnBuiltUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User"
            };
            _userBuilder.SetUsername(user.UserName)
                        .SetEmail(user.Email)
                        .SetFirstName(user.FirstName)
                        .SetLastName(user.LastName);

            // Act
            var result = _userBuilder.Build();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserName, result.UserName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task Build_ShouldThrowException_WhenFirstNameIsMissing()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                LastName = "User"
            };
            _userBuilder.SetUsername(user.UserName)
                        .SetEmail(user.Email)
                        .SetLastName(user.LastName);

            // Act
            await _userBuilder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task Build_ShouldThrowException_WhenLastNameIsMissing()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                FirstName = "Test"
            };
            _userBuilder.SetUsername(user.UserName)
                        .SetEmail(user.Email)
                        .SetFirstName(user.FirstName);

            // Act
            await _userBuilder.Build();
        }

        [TestMethod]
        public void SetEmail_ShouldSetEmail()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            _userBuilder.SetEmail(email);

            // Assert
            Assert.AreEqual(email, _userBuilder.User.Email);
        }

        [TestMethod]
        public void SetUsername_ShouldSetUsername()
        {
            // Arrange
            var username = "testuser";

            // Act
            _userBuilder.SetUsername(username);

            // Assert
            Assert.AreEqual(username, _userBuilder.User.UserName);
        }

        [TestMethod]
        public void SetPassword_ShouldSetPasswordHash()
        {
            // Arrange
            var password = "password";

            // Act
            _userBuilder.SetPassword(password);

            // Assert
            Assert.AreEqual(password, _userBuilder.User.PasswordHash);
        }

        [TestMethod]
        public void AddRole_ShouldAddRole()
        {
            // Arrange
            var role = new Role { Name = "Admin" };

            // Act
            _userBuilder.AddRole(role);

            // Assert
            Assert.IsTrue(_userBuilder.User.Role.Contains(role));
        }

        [TestMethod]
        public void AddRoles_ShouldAddRoles()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" }
            };

            // Act
            _userBuilder.AddRoles(roles);

            // Assert
            Assert.IsTrue(_userBuilder.User.Role.Contains(roles[0]));
            Assert.IsTrue(_userBuilder.User.Role.Contains(roles[1]));
        }

        [TestMethod]
        public void AddPermission_ShouldAddPermission()
        {
            // Arrange
            var permission = new Permission { Name = "Read" };

            // Act
            _userBuilder.AddPermission(permission);

            // Assert
            Assert.IsTrue(_userBuilder.User.Permissions.Contains(permission));
        }

        [TestMethod]
        public void AddPermissions_ShouldAddPermissions()
        {
            // Arrange
            var permissions = new List<Permission>
            {
                new Permission { Name = "Read" },
                new Permission { Name = "Write" }
            };

            // Act
            _userBuilder.AddPermissions(permissions);

            // Assert
            Assert.IsTrue(_userBuilder.User.Permissions.Contains(permissions[0]));
            Assert.IsTrue(_userBuilder.User.Permissions.Contains(permissions[1]));
        }

        [TestMethod]
        public void SetPhoneNumber_ShouldSetPhoneNumber()
        {
            // Arrange
            var phoneNumber = "1234567890";

            // Act
            _userBuilder.SetPhoneNumber(phoneNumber);

            // Assert
            Assert.AreEqual(phoneNumber, _userBuilder.User.PhoneNumber);
        }

        [TestMethod]
        public void SetFirstName_ShouldSetFirstName()
        {
            // Arrange
            var firstName = "Test";

            // Act
            _userBuilder.SetFirstName(firstName);

            // Assert
            Assert.AreEqual(firstName, _userBuilder.User.FirstName);
        }

        [TestMethod]
        public void SetLastName_ShouldSetLastName()
        {
            // Arrange
            var lastName = "User";

            // Act
            _userBuilder.SetLastName(lastName);

            // Assert
            Assert.AreEqual(lastName, _userBuilder.User.LastName);
        }
    }
}
