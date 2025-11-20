using HybridWork.Models;
using HybridWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Services
{
    public class WorkstationService
    {
        private readonly IWorkstationRepository _repo;

        public WorkstationService(IWorkstationRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Workstation>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Workstation?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
        public Task<Workstation> CreateAsync(Workstation w) => _repo.CreateAsync(w);
        public Task<bool> UpdateAsync(string id, Workstation w) => _repo.UpdateAsync(id, w);
        public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);
    }
}
