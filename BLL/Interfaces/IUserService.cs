namespace BLL.Interfaces
{
    public interface IUserService
    {
        bool Login(string username, string password);
        bool Register(string username, string password);
    }
}
