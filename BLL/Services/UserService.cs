using BLL.Interfaces;
using Common.Logging;
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
            Log.Info($"Login attempt: {username}");

            var user = _repo.GetByUsername(username);
            if (user == null)
            {
                Log.Warn($"Login failed: user not found ({username})");
                return false;
            }

            var ok = user.PasswordHash == Hash(password);
            if (!ok)
                Log.Warn($"Login failed: wrong password ({username})");
            else
                Log.Info($"Login success ({username})");

            return ok;
        }

        public bool Register(string username, string password)
        {
            Log.Info($"Register attempt: {username}");

            if (_repo.Exists(username))
            {
                Log.Warn($"Register failed: user exists ({username})");
                return false;
            }

            _repo.Add(new User
            {
                Username = username,
                PasswordHash = Hash(password)
            });

            Log.Info($"User registered: {username}");
            return true;
        }

        public User? GetUser(string username)
        {
            Log.Info($"GetUser: {username}");
            return _repo.GetByUsername(username);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            Log.Info($"ChangePassword: userId={userId}");

            var user = _repo.GetById(userId);
            if (user == null)
            {
                Log.Warn($"ChangePassword failed: user not found (id={userId})");
                return false;
            }

            if (user.PasswordHash != Hash(oldPassword))
            {
                Log.Warn($"ChangePassword failed: wrong old password (id={userId})");
                return false;
            }

            _repo.UpdatePassword(userId, Hash(newPassword));
            Log.Info($"Password changed (id={userId})");
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
