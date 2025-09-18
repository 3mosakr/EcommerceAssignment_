using Ecommerce.Services.Implementations;
using Ecommerce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Tests
{
    public class PasswordServiceTests
    {
        private readonly IPasswordService _passwordService;

        public PasswordServiceTests()
        {
            _passwordService = new PasswordService();
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedValue()
        {
            var password = "Test123!";
            var hashed = _passwordService.HashPassword(password);

            Assert.NotNull(hashed);
            Assert.NotEqual(password, hashed);
            Assert.True(hashed.Length > 20);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            var password = "Test123!";
            var hashed = _passwordService.HashPassword(password);

            var result = _passwordService.VerifyPassword(hashed, password);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsWrong()
        {
            var password = "Test123!";
            var hashed = _passwordService.HashPassword(password);

            var result = _passwordService.VerifyPassword(hashed, "WrongPass");

            Assert.False(result);
        }
    }
}
