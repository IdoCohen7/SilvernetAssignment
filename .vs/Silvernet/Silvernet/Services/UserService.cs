using Microsoft.EntityFrameworkCore;
using Silvernet.Data;
using Silvernet.DTOs;
using Silvernet.Models;

namespace Silvernet.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDBContext dBContext, ILogger<UserService> _logger)
        {
            this._dbContext = dBContext;
            this._logger = _logger;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByTenantId(long id)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch users for tenant: ", id);
                var users = await _dbContext.Users.Where(u => u.TenantId == id).AsNoTracking().ToListAsync();
                if (users == null)
                {
                    return null;
                }

                return users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    CreationDate = u.CreationDate,
                    TenantId = u.TenantId,

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message, "Failed to fetch all users by tenant: ", id);
                throw;
            }
        }

    }
}
