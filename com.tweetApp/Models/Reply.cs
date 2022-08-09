using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Models
{
    public class Reply
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("replyText")]
        public string ReplyText { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("tweetId")]
        public string TweetId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
