using Silvernet.Models;
using Silvernet.DTOs;

namespace Silvernet.Services
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantDTO>> GetAllTenants();
        Task<TenantDTO> GetTenantByIdAsync(long id);
        Task<TenantDTO> CreateTenantAsync(TenantCreateDTO tenantDTO);
        Task<bool> UpdateTenantAsync(long id, TenantUpdateDTO tenantDTO);
        Task<bool> DeleteTenantAsync(long id);

    }
}
