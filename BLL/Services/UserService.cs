using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo = new();

        public bool Login(string username, string password)
        {
            var user = _repo.GetByUsername(username);
            if (user == null) return false;

            return user.PasswordHash == Hash(password);
        }

        public bool Register(string username, string password)
        {
            if (_repo.Exists(username))
                return false;

            _repo.Add(new User
            {
                Username = username,
                PasswordHash = Hash(password)
            });

            return true;
        }

        public User? GetUser(string username)
        {
            return _repo.GetByUsername(username);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _repo.GetById(userId);
            if (user == null) return false;

            if (user.PasswordHash != Hash(oldPassword))
                return false;

            _repo.UpdatePassword(userId, Hash(newPassword));
            return true;
        }

        private string Hash(string value)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(bytes);
        }
    }
}
