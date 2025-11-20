using HybridWork.Models;
using HybridWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Employee>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Employee?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
        public Task<Employee> CreateAsync(Employee e) => _repo.CreateAsync(e);
        public Task<bool> UpdateAsync(string id, Employee e) => _repo.UpdateAsync(id, e);
        public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);
    }
}
