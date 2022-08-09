using com.tweetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(string id);
        User CreateUser(User user);
        public User Login(string username, string password);
        public User SearchUsername(string username);
        void UpdateUser(string id, User user);
        void RemoveUser(string id);

    }
}
