using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.DAO
{
    public class TweetAppDatabaseSettings : ITweetAppDatabaseSettings
    {
        public string UserCollectionName { get; set; } = string.Empty;
        public string TweetCollectionName { get; set; } = string.Empty;
        public string ReplyCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
