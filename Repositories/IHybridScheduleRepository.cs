using HybridWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public interface IHybridScheduleRepository
    {
        Task<IEnumerable<HybridSchedule>> GetAllAsync();
        Task<HybridSchedule?> GetByIdAsync(string id);
        Task<HybridSchedule> CreateAsync(HybridSchedule schedule);
        Task<bool> UpdateAsync(string id, HybridSchedule schedule);
        Task<bool> DeleteAsync(string id);
    }
}
