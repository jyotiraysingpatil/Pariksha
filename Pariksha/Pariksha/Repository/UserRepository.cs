using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using Pariksha.DTO;
using Pariksha.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Pariksha.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }
        public async Task<bool> AddUser(Users users)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    // Remove user_id from insert query since it's auto-incremented
                    string query = "INSERT INTO users (firstName, lastName, isActive, password, phoneNumber, username) VALUES (@firstName, @lastName, @isActive, @password, @phoneNumber, @username)";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@firstName", users.firstName);
                    command.Parameters.AddWithValue("@lastName", users.lastName);
                    command.Parameters.AddWithValue("@isActive", users.isActive);
                    command.Parameters.AddWithValue("@password", users.password); // Ensure password is hashed
                    command.Parameters.AddWithValue("@phoneNumber", users.phoneNumber);
                    command.Parameters.AddWithValue("@username", users.username);

                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                    return false;
                }
            }
        }

        public async Task<bool> DeleteUser(int user_id)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "delete from users where user_id=@user_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception )
                {
                    return false;
                }
            }
        }

        public async Task<Users> FindByFirstName(string firstName)
        {
           using(MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "Select * from users where firstName=@firstName";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@firstName",firstName);
                    await c.OpenAsync();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Users
                            {
                                user_id = reader.GetInt32("user_id"),
                                firstName = reader.GetString("firstName"),
                                lastName = reader.GetString("lastName"),
                                isActive = reader.GetBoolean("isActive"),
                                password = reader.GetString("password"),
                                phoneNumber = reader.GetString("phoneNumber"),
                                username = reader.GetString("username")
                            };
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while finding");
                }
            }return null;
        }

        public async Task<Users> FindUserById(int user_id)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM users WHERE user_id=@user_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    await c.OpenAsync();
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Users
                            {
                                user_id = reader.GetInt32("user_id"),
                                firstName = reader.GetString("firstName"),
                                lastName = reader.GetString("lastName"),
                                isActive = reader.GetBoolean("isActive"),
                                password = reader.GetString("password"),
                                phoneNumber = reader.GetString("phoneNumber"),
                                username = reader.GetString("username")
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while finding the user", ex);
                }
                return null;
            }
        }
        public async Task<List<Users>> GetAll()
        {
            List<Users> users = new List<Users>();
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                string query = "select * from users";
                MySqlCommand command = new MySqlCommand(query, c);
                await c.OpenAsync();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new Users
                        {
                            user_id = reader.GetInt32("user_id"),
                            firstName = reader.GetString("firstName"),
                            lastName = reader.GetString("lastName"),
                            isActive = reader.GetBoolean("isActive"),
                            password = reader.GetString("password"),
                            phoneNumber = reader.GetString("phoneNumber"),
                            username = reader.GetString("username")
                        });
                    }
                }
            }
            return users;
        }

        public async Task<Users> RegisterUserAsync(UserRegistrationDto userDto)
        {
            const string checkQuery = "SELECT COUNT(1) FROM users WHERE username = @username";
            const string insertQuery = "INSERT INTO users (firstName, lastName, isActive, password, phoneNumber, username) VALUES (@firstName, @lastName, @isActive, @password, @phoneNumber, @username)";

            using var connection = new MySqlConnection(_connectionString);
            using var checkCommand = new MySqlCommand(checkQuery, connection);
            using var insertCommand = new MySqlCommand(insertQuery, connection);

            checkCommand.Parameters.AddWithValue("@username", userDto.Username);

            try
            {
                await connection.OpenAsync();
                var exists = Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0;

                if (exists)
                {
                    throw new Exception("Username already exists");
                }

                insertCommand.Parameters.AddWithValue("@firstName", userDto.FirstName);
                insertCommand.Parameters.AddWithValue("@lastName", userDto.LastName);
                insertCommand.Parameters.AddWithValue("@isActive", true);
                insertCommand.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(userDto.Password));
                insertCommand.Parameters.AddWithValue("@phoneNumber", userDto.PhoneNumber);
                insertCommand.Parameters.AddWithValue("@username", userDto.Username);

                var rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    var newUserId = (int)insertCommand.LastInsertedId;
                    return new Users
                    {
                        user_id = newUserId,
                        firstName = userDto.FirstName,
                        lastName = userDto.LastName,
                        isActive = true,
                        password = insertCommand.Parameters["@password"].Value.ToString(),
                        phoneNumber = userDto.PhoneNumber,
                        username = userDto.Username
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user", ex);
            }
            return null;
        }
        
    

        public async Task<bool> UpdateUser(Users users)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "update users set firstName=@firstName,lastName=@lastName,isActive=@isActive,password=@password,phoneNumber=@phoneNumber,username=@username where user_id=@user_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    
                    command.Parameters.AddWithValue("@firstName", users.firstName);
                    command.Parameters.AddWithValue("@lastName", users.lastName);
                    command.Parameters.AddWithValue("@isActive", users.isActive);
                    command.Parameters.AddWithValue("@password", users.password);
                    command.Parameters.AddWithValue("@phoneNumber", users.phoneNumber);
                    command.Parameters.AddWithValue("@username", users.username);
                    command.Parameters.AddWithValue("@user_id", users.user_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception )
                {
                    return false;
                }

            }
        }
    }
}
