using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HybridWork.Models
{
    public class WorkspaceReservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string WorkstationId { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Optional: reservation time slots
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public string Status { get; set; } = "Confirmed";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
