using HybridWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public interface IWorkspaceReservationRepository
    {
        Task<IEnumerable<WorkspaceReservation>> GetAllAsync();
        Task<WorkspaceReservation?> GetByIdAsync(string id);
        Task<WorkspaceReservation> CreateAsync(WorkspaceReservation reservation);
        Task<bool> UpdateAsync(string id, WorkspaceReservation reservation);
        Task<bool> DeleteAsync(string id);
    }
}
