using Application.Abstractions;
using Application.Services;
using Domain.Models;
using Moq;

namespace InfrastructureTests.Services
{
    public class RefreshTokenServiceTests
    {
        Mock<IRefreshTokenRepository> repoMock;
        public RefreshTokenServiceTests()
        {
            repoMock = new Mock<IRefreshTokenRepository>();
        }

        [Theory]
        [ClassData(typeof(RefreshTokenServiceTestsData))]
        public async Task CreateRefreshTokenAsync_ShouldReturnRefreshToken(CustomUser user)
        {
            //Arrange
            var refTokServ = new RefreshTokenService(repoMock.Object);

            //Act
            var token = await refTokServ.CreateRefreshTokenAsync(user, CancellationToken.None);

            //Assert
            Assert.NotNull(token);
            Assert.IsType<RefreshToken>(token);
            repoMock.Verify(r => r.CreateTokenAsync(token, CancellationToken.None), Times.Once);
        }
    }
}
