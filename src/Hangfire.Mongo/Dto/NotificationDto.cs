using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hangfire.Mongo.Dto
{
#pragma warning disable 1591
    public enum NotificationType
    {
        JobEnqueued = 0,
    }
    
    public class NotificationDto
    {
        public static NotificationDto JobEnqueued(string queue)
        {
            return new NotificationDto
            {
                Id = ObjectId.GenerateNewId(),
                Type = NotificationType.JobEnqueued,
                Value = queue
            };
        }

        public NotificationDto()
        {

        }
        public NotificationDto(BsonDocument doc)
        {
            Id = doc["_id"].AsObjectId;
            Type = (NotificationType)doc[nameof(Type)].AsInt32;
            if (doc.TryGetValue(nameof(Value), out var value))
            {
                Value = value.StringOrNull();
            }
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public NotificationType Type { get; set; }
        
        public string Value { get; set; }

        public BsonDocument Serialize()
        {
            return new BsonDocument
            {
                ["_id"] = Id,
                [nameof(Type)] = (int)Type,
                [nameof(Value)] = Value.ToBsonValue(),
            };
        }
    }
#pragma warning restore 1591
}