using HybridWork.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _collection;

        public EmployeeRepository(IMongoClient client, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var dbName = configuration.GetSection("MongoDBSettings:DatabaseName").Value ?? "HybridWorkDB";
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<Employee>("Employees");
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _collection.Find(Builders<Employee>.Filter.Empty).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _collection.InsertOneAsync(employee);
            return employee;
        }

        public async Task<bool> UpdateAsync(string id, Employee employee)
        {
            employee.UpdatedAt = System.DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(e => e.Id == id, employee);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(e => e.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
