using com.tweetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Services
{
    public interface IReplyService
    {
        Reply CreateReply(Reply reply);
        List<Reply> GetRepliesInTweet(string tid);
        Reply GetReplyById(string id);
    }
}
