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

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("INFO: Attempting to fetch all users");
                var users = await _dbContext.Users.AsNoTracking().ToListAsync();

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
                _logger.LogWarning(ex.Message, "Failed to fetch all users");
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(long id)
        {
            try
            {
                var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return null;
                }

                return new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    CreationDate = user.CreationDate,
                    TenantId = user.TenantId,
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to fetch user");
                throw;
            }
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO userDTO)
        {
            var newUser = new User
            {
                Id = userDTO.Id, // User-provided Israeli ID
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                TenantId = userDTO.TenantId,
            };

            try
            {
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User created. ID: {Id}", newUser.Id);

                return new UserDTO
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Phone = newUser.Phone,
                    CreationDate = newUser.CreationDate,
                    TenantId = newUser.TenantId,
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to create user");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(long id, UserUpdateDTO userDTO)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);

            // no matching foreign key

            var tenant = await _dbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.Id == userDTO.TenantId);

            if (tenant == null)
            {
                return false;
            }


            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.Email = userDTO.Email;
            existingUser.Phone = userDTO.Phone;
            existingUser.TenantId = userDTO.TenantId;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                return false;
            }

            try
            {
                _dbContext.Users.Remove(existingUser);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user ID: {Id}", id);
                throw;
            }
        }
    }
}
