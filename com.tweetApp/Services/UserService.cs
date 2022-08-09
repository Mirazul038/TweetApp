using com.tweetApp.DAO;
using com.tweetApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> users;

        public UserService(ITweetAppDatabaseSettings settings, IMongoClient mongoClient){
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            users = database.GetCollection<User>(settings.UserCollectionName);
        }
        public User CreateUser(User user)
        {
            users.InsertOne(user);
            return user;
        }

        public User GetUserById(string id)
        {
            return users.Find(user => user.Id == id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return users.Find(user => true).ToList(); 
        }

        public User Login(string username, string password)
        {
            return users.Find(user => user.Username == username && user.Password == password).FirstOrDefault();
        }

        public void RemoveUser(string id)
        {
            users.DeleteOne(user => user.Id == id);
        }

        public User SearchUsername(string username)
        {
            return users.Find(user => user.Username == username).FirstOrDefault();
        }

        public void UpdateUser(string id, User user)
        {
            users.ReplaceOne(user => user.Id == id, user);
        }
    }
}
