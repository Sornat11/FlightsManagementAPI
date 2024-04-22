using Xunit;
using FakeItEasy;
using FlightsManagementAPI.Controllers;
using FlightsManagementAPI.Services;
using FlightsManagementAPI.Models;
using System.Threading.Tasks;
using FlightsManagementAPI.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace FlightsManagementAPI.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly IAuthService _fakeAuthService;

        public AuthControllerTests()
        {
            _fakeAuthService = A.Fake<IAuthService>();
            _controller = new AuthController(_fakeAuthService);
        }

        [Fact]
        public async Task Register_WithValidCredentials_ReturnsUser()
        {
            // Arrange
            var validUser = new UserDto { Username = "validUser123", Password = "ValidPass123!" };
            var expectedUser = new User { Username = validUser.Username };
            A.CallTo(() => _fakeAuthService.Register(validUser)).Returns(Task.FromResult(expectedUser));

            // Act
            var result = await _controller.Register(validUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedUser, okResult.Value);
            A.CallTo(() => _fakeAuthService.Register(validUser)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Register_WithInvalidUsernameLength_ReturnsBadRequest()
        {
            // Arrange
            var invalidUser = new UserDto { Username = "usr", Password = "ValidPass123!" };  // Za krótka nazwa u¿ytkownika
            _controller.ModelState.AddModelError("Username", "Username must be between 5 and 20 characters.");

            // Act
            var result = await _controller.Register(invalidUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Contains("Username must be between 5 and 20 characters.", _controller.ModelState["Username"].Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task Register_WithInvalidPasswordFormat_ReturnsBadRequest()
        {
            // Arrange
            var invalidUser = new UserDto { Username = "validUser123", Password = "short" }; // Nieprawid³owy format has³a
            _controller.ModelState.AddModelError("Password", "Password must contain at least one uppercase letter, one lowercase letter, and one digit.");

            // Act
            var result = await _controller.Register(invalidUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Contains("Password must contain at least one uppercase letter, one lowercase letter, and one digit.", _controller.ModelState["Password"].Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var validCredentials = new UserDto { Username = "testuser", Password = "TestPassword123!" };
            var expectedToken = "ExpectedToken";
            A.CallTo(() => _fakeAuthService.Login(validCredentials)).Returns(Task.FromResult(expectedToken));

            // Act
            var result = await _controller.Login(validCredentials);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedToken, okResult.Value);
            A.CallTo(() => _fakeAuthService.Login(validCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var invalidCredentials = new UserDto { Username = "testuser", Password = "wrongpassword" };
            A.CallTo(() => _fakeAuthService.Login(invalidCredentials)).Returns(Task.FromResult<string>(null));

            // Act
            var result = await _controller.Login(invalidCredentials);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            A.CallTo(() => _fakeAuthService.Login(invalidCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Login_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var credentialsWithInvalidModelState = new UserDto { Username = "user", Password = "pass" }; // Za³o¿enie, ¿e to narusza walidacje modelu
            _controller.ModelState.AddModelError("Password", "Password is too short.");

            // Act
            var result = await _controller.Login(credentialsWithInvalidModelState);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Contains("Password is too short.", _controller.ModelState["Password"].Errors[0].ErrorMessage);
        }
    }
}