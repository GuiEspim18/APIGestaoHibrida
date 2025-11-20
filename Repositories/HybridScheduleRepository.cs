using HybridWork.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public class HybridScheduleRepository : IHybridScheduleRepository
    {
        private readonly IMongoCollection<HybridSchedule> _collection;

        public HybridScheduleRepository(IMongoClient client, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var dbName = configuration.GetSection("MongoDBSettings:DatabaseName").Value ?? "HybridWorkDB";
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<HybridSchedule>("HybridSchedules");
        }

        public async Task<IEnumerable<HybridSchedule>> GetAllAsync()
        {
            return await _collection.Find(Builders<HybridSchedule>.Filter.Empty).ToListAsync();
        }

        public async Task<HybridSchedule?> GetByIdAsync(string id)
        {
            return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<HybridSchedule> CreateAsync(HybridSchedule schedule)
        {
            await _collection.InsertOneAsync(schedule);
            return schedule;
        }

        public async Task<bool> UpdateAsync(string id, HybridSchedule schedule)
        {
            schedule.UpdatedAt = System.DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(s => s.Id == id, schedule);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(s => s.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
