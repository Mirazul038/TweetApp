using com.tweetApp.DAO;
using com.tweetApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMongoCollection<Tweet> _tweets;

        public TweetService(ITweetAppDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _tweets = database.GetCollection<Tweet>(settings.TweetCollectionName);
        }

        public Tweet CreateTweet(Tweet tweet)
        {
            _tweets.InsertOne(tweet);
            return tweet;
        }

        public List<Tweet> GetAllTweets()
        {
            return _tweets.Find(tweet => true).ToList();
        }

        public Tweet GetTweetById(string id)
        {
            return _tweets.Find(tweet => tweet.Id == id).FirstOrDefault();
        }

        public List<Tweet> GetTweetOfUser(string uid)
        {
            return _tweets.Find(tweet => tweet.UserId == uid).ToList();
        }

        public void RemoveTweet(string id)
        {
            _tweets.DeleteOne(tweet => tweet.Id == id);
        }

        public void UpdateTweet(string id, Tweet tweet)
        {
            _tweets.ReplaceOne(tweet => tweet.Id == id, tweet);
        }
    }
}
