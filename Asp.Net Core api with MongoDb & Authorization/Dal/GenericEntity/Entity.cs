using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace $safeprojectname$.GenericEntity
{
    [BsonIgnoreExtraElements]
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public string UpdatedBy { get; set; }
    }
}