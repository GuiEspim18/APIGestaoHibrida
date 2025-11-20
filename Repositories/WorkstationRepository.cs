using HybridWork.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public class WorkstationRepository : IWorkstationRepository
    {
        private readonly IMongoCollection<Workstation> _collection;

        public WorkstationRepository(IMongoClient client, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var dbName = configuration.GetSection("MongoDBSettings:DatabaseName").Value ?? "HybridWorkDB";
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<Workstation>("Workstations");
        }

        public async Task<IEnumerable<Workstation>> GetAllAsync()
        {
            var filter = Builders<Workstation>.Filter.Empty;
            var list = await _collection.Find(filter).ToListAsync();
            return list;
        }

        public async Task<Workstation?> GetByIdAsync(string id)
        {
            var filter = Builders<Workstation>.Filter.Eq(w => w.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Workstation> CreateAsync(Workstation workstation)
        {
            await _collection.InsertOneAsync(workstation);
            return workstation;
        }

        public async Task<bool> UpdateAsync(string id, Workstation workstation)
        {
            workstation.UpdatedAt = System.DateTime.UtcNow;
            var replaceResult = await _collection.ReplaceOneAsync(w => w.Id == id, workstation);
            return replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await _collection.DeleteOneAsync(w => w.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
