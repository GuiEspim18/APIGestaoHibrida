using HybridWork.Models;
using HybridWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Services
{
    public class HybridScheduleService
    {
        private readonly IHybridScheduleRepository _repo;

        public HybridScheduleService(IHybridScheduleRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<HybridSchedule>> GetAllAsync() => _repo.GetAllAsync();
        public Task<HybridSchedule?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
        public Task<HybridSchedule> CreateAsync(HybridSchedule s) => _repo.CreateAsync(s);
        public Task<bool> UpdateAsync(string id, HybridSchedule s) => _repo.UpdateAsync(id, s);
        public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);
    }
}
