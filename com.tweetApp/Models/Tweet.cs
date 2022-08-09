using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("tweetText")]
        public string TweetText { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

    }
}
