using Pariksha.DTO;
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface IUserRepository
    {
         Task<bool> AddUser(Users users);
        Task<bool> UpdateUser(Users users);
        Task<bool> DeleteUser(int user_id);
        Task<List<Users>> GetAll();
        Task<Users> RegisterUserAsync(UserRegistrationDto userDto);
    }
}
