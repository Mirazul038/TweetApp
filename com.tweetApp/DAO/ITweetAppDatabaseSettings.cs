using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.DAO
{
    public interface ITweetAppDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string TweetCollectionName { get; set; }
        string ReplyCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
