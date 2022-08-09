using com.tweetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public interface ITweetService
    {
        Tweet CreateTweet(Tweet tweet);
        public Tweet GetTweetById(string id);
        public List<Tweet> GetAllTweets();
        public List<Tweet> GetTweetOfUser(string uid);
        public void RemoveTweet(string id);
        public void UpdateTweet(string id, Tweet tweet);
    }
}
