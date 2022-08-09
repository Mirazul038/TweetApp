using com.tweetApp.DAO;
using com.tweetApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IMongoCollection<Reply> _replies;

        public ReplyService(ITweetAppDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _replies = database.GetCollection<Reply>(settings.ReplyCollectionName);
        }

        public Reply CreateReply(Reply reply)
        {
            _replies.InsertOne(reply);
            return reply;
        }

        public List<Reply> GetRepliesInTweet(string tid)
        {
            return _replies.Find(reply => reply.TweetId == tid).ToList();
        }

        public Reply GetReplyById(string id)
        {
            return _replies.Find(reply => reply.Id == id).FirstOrDefault();
        }
    }
}
