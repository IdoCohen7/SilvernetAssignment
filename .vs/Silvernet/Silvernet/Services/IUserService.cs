using Silvernet.DTOs;

namespace Silvernet.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersByTenantId(long id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByIdAsync(long id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO userDTO);
        Task<bool> UpdateUserAsync(long id, UserUpdateDTO userDTO);
        Task<bool> DeleteUserAsync(long id);
    }
}
