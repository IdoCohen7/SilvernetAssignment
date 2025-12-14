using Microsoft.EntityFrameworkCore;
using Silvernet.Data;
using Silvernet.Models;
using Silvernet.DTOs;

namespace Silvernet.Services
{
    public class TenantService : ITenantService
    {

        private readonly AppDBContext _dbContext;
        private readonly ILogger<TenantService> _logger;

        public TenantService(AppDBContext dBContext, ILogger<TenantService> _logger)
        {
            this._dbContext = dBContext;
            this._logger = _logger;
        }

        public async Task<IEnumerable<TenantDTO>> GetAllTenants()
        {
            try
            {
                _logger.LogInformation("INFO: Attemping to fetch all tenants");
                var tenants = await _dbContext.Tenants.AsNoTracking().ToListAsync();

                if (tenants == null)
                {
                    return null;
                }

                return tenants.Select(t=> new TenantDTO
                {
                    Id = t.Id, // depends
                    Name = t.Name,
                    Email = t.Email,
                   Phone = t.Phone,
                   CreationDate = t.CreationDate,

                    

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message, "Failed to fetch all tenants");
                throw;

            }

        }

        public async Task<TenantDTO> GetTenantByIdAsync(long id)
        {
            try
            {
                var tenant = await _dbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

                if (tenant == null)
                {
                    return null;
                }

                return new TenantDTO
                {
                    Id = tenant.Id,
                    Name = tenant.Name,
                    Email = tenant.Email,
                    Phone = tenant.Phone,
                    CreationDate = tenant.CreationDate,
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to fetch tenant");
                throw;
            }
        }

        public async Task<TenantDTO> CreateTenantAsync(TenantCreateDTO tenenatDTO)
        {
            var newTenant = new Tenant
            {
                Id = tenenatDTO.Id, // User-provided Israeli ID
                Name = tenenatDTO.Name,
                Email = tenenatDTO.Email,
                Phone = tenenatDTO.Phone,
            };
            
            try
            {
                _dbContext.Tenants.Add(newTenant);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Tenant created. ID: {Id}", newTenant.Id);

                return new TenantDTO
                {
                    Id = newTenant.Id,
                    Name = newTenant.Name,
                    Email = newTenant.Email,
                    Phone = newTenant.Phone,
                    CreationDate = newTenant.CreationDate,
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to create tenant");
                throw;

            }
        }

        public async Task<bool> UpdateTenantAsync(long id, TenantUpdateDTO tenantDTO)
        {
            var existingTenant = await _dbContext.Tenants.FindAsync(id);

            if (existingTenant == null)
            {
                return false;
            }

            existingTenant.Name = tenantDTO.Name;
            existingTenant.Email = tenantDTO.Email;
            existingTenant.Phone = tenantDTO.Phone;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update tenant ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteTenantAsync(long id)
        {
            var existingTenant = await _dbContext.Tenants.FindAsync(id);

            if (existingTenant == null)
            {
                return false;
            }

            try
            {
                _dbContext.Tenants.Remove(existingTenant);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete tenant ID: {Id}", id);
                throw;
            }
        }





    }
}
