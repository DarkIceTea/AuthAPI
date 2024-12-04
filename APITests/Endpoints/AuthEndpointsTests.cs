using API.Endpoints;
using Application.Commands.LoginUser;
using Application.Commands.RegisterUser;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace APITests.Endpoints
{
    public class AuthEndpointsTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly Mock<IValidator<RegisterUserCommand>> _mockValidator;

        public AuthEndpointsTests()
        {
            _mockSender = new Mock<ISender>();
            _mockValidator = new Mock<IValidator<RegisterUserCommand>>();
            AuthEndpoints.SetSender(_mockSender.Object);
        }

        [Fact]
        public async Task Registration_ShouldReturnTokens_WhenValidationPasses()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                UserName = "Konstantin",
                Password = "12345Hq*",
                Email = "konstantin@mail.com",
                UserRole = "patient"
            };
            var cancellationToken = CancellationToken.None;

            _mockValidator
                .Setup(v => v.ValidateAsync(command, cancellationToken))
                .ReturnsAsync(new ValidationResult());

            //_mockSender
            //    .Setup(s => s.Send(command, cancellationToken))
            //    .ReturnsAsync(expectedTokens);

            // Act
            var result = await AuthEndpoints.Registration(command, _mockValidator.Object, cancellationToken);

            // Assert
            Assert.NotNull(result);
            _mockValidator.Verify(v => v.ValidateAsync(command, cancellationToken), Times.Once);
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Registration_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                UserName = "Konstantin",
                Password = "12345Hq*",
                Email = "",
                UserRole = "patient"

            };
            var cancellationToken = CancellationToken.None;

            _mockValidator
                .Setup(v => v.ValidateAsync(command, cancellationToken))
                .ReturnsAsync(new ValidationResult(new[]
                {
                new ValidationFailure("Property", "Error message")
                }));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => AuthEndpoints.Registration(command, _mockValidator.Object, cancellationToken));
            _mockValidator.Verify(v => v.ValidateAsync(command, cancellationToken), Times.Once);
            _mockSender.Verify(s => s.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Login_ShouldReturnTokens_WhenCalled()
        {
            // Arrange
            var command = new LoginUserCommand { /* заполняем свойства */ };
            var cancellationToken = CancellationToken.None;

            //_mockSender
            //    .Setup(s => s.Send(command, cancellationToken))
            //    .ReturnsAsync(expectedTokens);

            // Act
            var result = await AuthEndpoints.Login(command, cancellationToken);

            // Assert
            Assert.NotNull(result);
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }

        //[Fact]
        //public async Task SignOut_ShouldCallSendMethod()
        //{
        //    // Arrange
        //    var command = new SignOutUserCommand { /* заполняем свойства */ };
        //    var cancellationToken = CancellationToken.None;

        //    // Act
        //    await AuthEndpoints.SignOut(command, cancellationToken);

        //    // Assert
        //    _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        //}

        //[Fact]
        //public async Task Refresh_ShouldReturnTokens_WhenCalled()
        //{
        //    // Arrange
        //    var command = new RefreshTokensCommand { /* заполняем свойства */ };
        //    var cancellationToken = CancellationToken.None;

        //    var expectedTokens = new Tokens { /* заполняем свойства */ };
        //    _mockSender
        //        .Setup(s => s.Send(command, cancellationToken))
        //        .ReturnsAsync(expectedTokens);

        //    // Act
        //    var result = await AuthEndpoints.Refresh(command, cancellationToken);

        //    // Assert
        //    Assert.Equal(expectedTokens, result);
        //    _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        //}
    }
}
