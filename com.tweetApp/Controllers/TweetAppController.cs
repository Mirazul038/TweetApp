using com.tweetApp.Models;
using com.tweetApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Controllers
{
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class TweetAppController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITweetService tweetService;
        private readonly IReplyService replyService;
        private readonly ILogger<TweetAppController> logger;

        public TweetAppController(IUserService userService, ITweetService tweetService, IReplyService replyService, ILogger<TweetAppController> logger)
        {
            this.userService = userService;
            this.tweetService = tweetService;
            this.replyService = replyService;
            this.logger = logger;

            logger.Log(LogLevel.Information, "hello from TweetAppController");
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<User> RegisterUser(User user)
        {
            try
            {
                logger.LogInformation($"Process of registration has now started for username : {user.Username}");
                userService.CreateUser(user);
                logger.LogInformation($"Registration is now successful for the username : {user.Username}");
                return CreatedAtAction(nameof(LoginUser), new { id = user.Id }, user);
            }
            catch (Exception e) {
                return BadRequest("Some pproblem occured in the server. Please try again after some time.");
            }
        }

        [HttpGet]
        [Route("login")]
        public ActionResult<User> LoginUser(string username, string password) 
        {
            try
            {
                var loggedInUser = userService.Login(username, password);
                if (loggedInUser != null)
                {
                    return loggedInUser;
                }
                else
                {
                    return NotFound($"Invalid credentials for the username : {username}");
                }
            }
            catch (Exception e) {
                return BadRequest("Some problem occured in the server. pplease try again later.");
            }
        }

        [HttpGet]
        [Route("{username}/forgot")]
        public ActionResult<User> ForgotPassword(string username, string newPassword)
        {
            try {
                var user = userService.SearchUsername(username);
                if (user == null)
                {
                    return NotFound($"User not found for username {username}");
                }
                else
                {
                    user.Password = newPassword;
                    userService.UpdateUser(user.Id, user);
                    return Ok($"Profile password updated for username {username}");
                }
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("users/all")]
        public ActionResult<List<User>> AllUsers() 
        {
            try {
                var users = userService.GetUsers();

                if (users != null)
                {
                    return users;
                }
                else
                {
                    return NotFound("There are no users existed");
                }
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("user/search/{username}")]
        public ActionResult<User> SearchUser(string username)
        {
            try {
                var user = userService.SearchUsername(username);
                if (user == null)
                {
                    return NotFound($"User not found for the username {username}");
                }
                else
                {
                    return user;
                }
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public ActionResult<string> GetUserById(string uid)
        {
            try {
                User user = userService.GetUserById(uid);
                return user.Username;
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        //Tweet Section
        [HttpGet]
        [Route("all")]
        public ActionResult<List<Tweet>> AllTweets()
        {
            try {
                var tweets = tweetService.GetAllTweets();
                if (tweets != null)
                {
                    return tweets;
                }
                else
                {
                    return NotFound("There are no tweets existed");
                }
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("{username}")]
        public ActionResult<List<Tweet>> GetAllTweetsOfUser(string username, string uid)
        {
            try {
                var tweets = tweetService.GetTweetOfUser(uid);

                if (tweets != null)
                {
                    return tweets;
                }
                else
                {
                    return NotFound($"There are no tweets existed for the username {username}");
                }
            }
            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpPost]
        [Route("{username}/add")]
        public ActionResult<Tweet> PostTweet(string username, [FromBody] Tweet tweet)
        {
            try
            {
                tweetService.CreateTweet(tweet);
                return CreatedAtAction(nameof(GetTweet), new { id = tweet.Id }, tweet);
            }

            catch(Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("GetTweetById")]
        public ActionResult<Tweet> GetTweet(string id)
        {
            try
            {
                var tweet = tweetService.GetTweetById(id);
                if (tweet == null)
                {
                    return NotFound($"Invalid tweet id : {id}");
                }
                else
                {
                    return tweet;
                }
            }

            catch(Exception e) {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpPut]
        [Route("{username}/update/{id}")]
        public ActionResult<Tweet> UpdateTweet(string username, string id, string newTweet)
        {
            try
            {
                var tweet = tweetService.GetTweetById(id);
                if (tweet == null)
                {
                    return NotFound($"Tweet not found for username {username}");
                }
                else
                {
                    tweet.TweetText = newTweet;
                    tweetService.UpdateTweet(id, tweet);
                    return Ok($"Tweet has updated!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpDelete]
        [Route("{username}/delete/{id}")]
        public ActionResult<Tweet> RemoveTweet(string username, string id) 
        {
            try
            {
                tweetService.RemoveTweet(id);
                return Ok("Tweet has been deleted");
            }

            catch(Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpPut]
        [Route("{username}/like/{id}")]
        public ActionResult<List<User>> LikeTweet(string username, string id)
        {
            try
            {
                var tweet = tweetService.GetTweetById(id);
                if (tweet == null)
                {
                    return NotFound("Tweet not found");
                }
                else
                {
                    tweet.Likes += 1;
                    tweetService.UpdateTweet(id, tweet);
                    return Ok("Someone liked your tweet");
                }
            }

            catch(Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpPost]
        [Route("{username}/reply")]
        public ActionResult<Reply> PostReply(string username, [FromBody] Reply reply)
        {
            try
            {
                replyService.CreateReply(reply);
                return CreatedAtAction(nameof(GetReply), new { id = reply.Id }, reply);
            }
            catch (Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("GetReplyById")]
        public ActionResult<Reply> GetReply(string id)
        {
            try
            {
                return replyService.GetReplyById(id);
            }

            catch (Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }

        [HttpGet]
        [Route("GetReplies")]
        public ActionResult<List<Reply>> GetAllReplyInTweet(string tid)
        {
            try
            {
                return replyService.GetRepliesInTweet(tid);
            }
            catch (Exception e)
            {
                return BadRequest("Some error occured at the server, please try again later");
            }
        }
    }
}
