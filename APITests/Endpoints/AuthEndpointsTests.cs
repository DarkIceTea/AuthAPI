using API.Endpoints;
using Application.Commands.LoginUser;
using Application.Commands.RefreshTokens;
using Application.Commands.RegisterUser;
using Application.Commands.SignOutUser;
using Application.Results;
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

        [Theory]
        [ClassData(typeof(AuthEndpointsTestsRegisterUserCommandData))]
        public async Task Registration_ShouldReturnTokens_WhenValidationPasses(RegisterUserCommand command)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockValidator
                .Setup(v => v.ValidateAsync(command, cancellationToken))
                .ReturnsAsync(new ValidationResult());

            _mockSender
                .Setup(s => s.Send(command, cancellationToken))
                .ReturnsAsync(new Tokens());

            // Act
            var result = await AuthEndpoints.Registration(command, _mockValidator.Object, cancellationToken);

            // Assert
            Assert.NotNull(result);
            _mockValidator.Verify(v => v.ValidateAsync(command, cancellationToken), Times.Once);
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }

        [Theory]
        [ClassData(typeof(AuthEndpointsTestsRegisterUserCommandData))]
        public async Task Registration_ShouldThrowException_WhenValidationFails(RegisterUserCommand command)
        {
            // Arrange
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

        [Theory]
        [ClassData(typeof(AuthEndpointsTestsLoginUserCommandData))]
        public async Task Login_ShouldReturnTokens_WhenCalled(LoginUserCommand command)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockSender
                .Setup(s => s.Send(command, cancellationToken))
                .ReturnsAsync(new Tokens());

            // Act
            var result = await AuthEndpoints.Login(command, cancellationToken);

            // Assert
            Assert.NotNull(result);
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task SignOut_ShouldCallSendMethod()
        {
            // Arrange
            var command = new SignOutUserCommand { /* заполняем свойства */ };
            var cancellationToken = CancellationToken.None;

            // Act
            await AuthEndpoints.SignOut(command, cancellationToken);

            // Assert
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Refresh_ShouldReturnTokens_WhenCalled()
        {
            // Arrange
            var command = new RefreshTokensCommand { /* заполняем свойства */ };
            var cancellationToken = CancellationToken.None;

            _mockSender
                .Setup(s => s.Send(command, cancellationToken))
                .ReturnsAsync(new Tokens());

            // Act
            var result = await AuthEndpoints.Refresh(command, cancellationToken);

            // Assert
            Assert.IsType<Tokens>(result);
            _mockSender.Verify(s => s.Send(command, cancellationToken), Times.Once);
        }
    }
}
