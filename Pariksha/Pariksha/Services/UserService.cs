using Pariksha.DTO;
using Pariksha.Entities;
using Pariksha.Repository;


namespace Pariksha.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Users>> GetAll() => await _repository.GetAll();
        public async Task<bool> AddUser(Users users) => await _repository.AddUser(users);
        public async Task<bool> UpdateUser(Users users) => await _repository.UpdateUser(users);
        public async Task<bool> DeleteUser(int user_id) => await _repository.DeleteUser(user_id);

        public async Task<Users> RegisterUserAsync(UserRegistrationDto userDto)
        {
            // Ensure validation logic for userDto as needed
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new ArgumentException("Username and Password cannot be empty");
            }

            try
            {
                return await _repository.RegisterUserAsync(userDto);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user", ex);
            }
        }
    }
}
