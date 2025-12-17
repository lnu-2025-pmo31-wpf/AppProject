using BLL.Services;
using Xunit;

namespace Tests.BLL
{
    public class UserServiceTests
    {
        [Fact]
        public void Login_WrongPassword_ReturnsFalse()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.Login("admin", "wrong_password");

            // Assert
            Assert.False(result);
        }
    }
}
