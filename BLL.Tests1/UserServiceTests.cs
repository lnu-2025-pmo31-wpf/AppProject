using BLL.Services;
using Xunit;

namespace Tests.BLL
{
    public class UserServiceTests
    {
        [Fact]
        public void Login_WithUnknownUser_ReturnsFalse()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.Login("this_user_does_not_exist_123", "12345");

            // Assert
            Assert.False(result);
        }
    }
}
