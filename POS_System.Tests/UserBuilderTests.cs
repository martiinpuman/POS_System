using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_System.Auth;
using POS_System.Auth.Models;
using POS_System.Auth.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace POS_System.Tests
{
    [TestClass]
    public class UserBuilderTests
    {
        private AuthDbContext _context;
        private UserBuilder _userBuilder;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AuthDbContext(options);
            _userBuilder = new UserBuilder(_context);
        }

        [TestMethod]
        public async Task Build_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
            _userBuilder.SetUsername(user.UserName).SetEmail(user.Email);

            // Act
            var result = await _userBuilder.Build();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserName, result.UserName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(1, await _context.Users.CountAsync());
        }

        [TestMethod]
        public async Task Build_ShouldReturnNull_WhenCancelled()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
            _userBuilder.SetUsername(user.UserName).SetEmail(user.Email);
            var cancellationToken = new CancellationToken(true);

            // Act
            var result = await _userBuilder.Build(cancellationToken);

            // Assert
            Assert.IsNull(result);
        }
    }
}
