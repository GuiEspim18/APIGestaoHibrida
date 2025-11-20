using HybridWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(string id);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> UpdateAsync(string id, Employee employee);
        Task<bool> DeleteAsync(string id);
    }
}
