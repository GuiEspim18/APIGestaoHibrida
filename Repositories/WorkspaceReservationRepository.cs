using HybridWork.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public class WorkspaceReservationRepository : IWorkspaceReservationRepository
    {
        private readonly IMongoCollection<WorkspaceReservation> _collection;

        public WorkspaceReservationRepository(IMongoClient client, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var dbName = configuration.GetSection("MongoDBSettings:DatabaseName").Value ?? "HybridWorkDB";
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<WorkspaceReservation>("WorkspaceReservations");
        }

        public async Task<IEnumerable<WorkspaceReservation>> GetAllAsync()
        {
            return await _collection.Find(Builders<WorkspaceReservation>.Filter.Empty).ToListAsync();
        }

        public async Task<WorkspaceReservation?> GetByIdAsync(string id)
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<WorkspaceReservation> CreateAsync(WorkspaceReservation reservation)
        {
            await _collection.InsertOneAsync(reservation);
            return reservation;
        }

        public async Task<bool> UpdateAsync(string id, WorkspaceReservation reservation)
        {
            reservation.UpdatedAt = System.DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(r => r.Id == id, reservation);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(r => r.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
