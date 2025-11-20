using HybridWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HybridWork.Repositories
{
    public interface IWorkstationRepository
    {
        Task<IEnumerable<Workstation>> GetAllAsync();
        Task<Workstation?> GetByIdAsync(string id);
        Task<Workstation> CreateAsync(Workstation workstation);
        Task<bool> UpdateAsync(string id, Workstation workstation);
        Task<bool> DeleteAsync(string id);
    }
}
