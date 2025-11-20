using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HybridWork.Models
{
    public class HybridSchedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; } = null!;

        // Ex: Monday, Tuesday, Wednesday...
        public string DayOfWeek { get; set; } = null!;

        // Ex: Remote, Office, Hybrid
        public string WorkMode { get; set; } = "Office";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
