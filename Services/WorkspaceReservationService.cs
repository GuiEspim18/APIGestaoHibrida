using HybridWork.Models;
using HybridWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Services
{
    public class WorkspaceReservationService
    {
        private readonly IWorkspaceReservationRepository _repo;

        public WorkspaceReservationService(IWorkspaceReservationRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<WorkspaceReservation>> GetAllAsync() => _repo.GetAllAsync();
        public Task<WorkspaceReservation?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
        public Task<WorkspaceReservation> CreateAsync(WorkspaceReservation r) => _repo.CreateAsync(r);
        public Task<bool> UpdateAsync(string id, WorkspaceReservation r) => _repo.UpdateAsync(id, r);
        public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);
    }
}
