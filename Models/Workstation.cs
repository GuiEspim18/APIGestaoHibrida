using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HybridWork.Models
{
    public class Workstation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        // Example statuses: Available, Occupied, Maintenance
        public string Status { get; set; } = "Available";

        // Optional: capacity, tags, etc.
        public int Capacity { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
