using Microsoft.AspNetCore.Razor.TagHelpers;
using MySql.Data.MySqlClient;
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public AdminRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public async Task<bool> AddAdmin(Admins admins)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "INSERT INTO admins (admin_id, first_name, last_name, username, password, is_active) VALUES (@admin_id, @first_name, @last_name, @username, @password, @is_active)";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@admin_id", admins.admin_id);
                    command.Parameters.AddWithValue("@first_name", admins.first_name);
                    command.Parameters.AddWithValue("@last_name", admins.last_name);
                    command.Parameters.AddWithValue("@username", admins.username);
                    command.Parameters.AddWithValue("@password", admins.password);
                    command.Parameters.AddWithValue("@is_active", admins.is_active);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    status = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
            return status;
        }


        public async Task<bool> DeleteAdmin(int admin_id)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "delete from admins where admin_id=@admin_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@admin_id", admin_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    status = rowsAffected > 0;
                }
                catch (Exception )
                {
                    throw;
                }
            }
            return status;
        }



        public async Task<List<Admins>> GetAll()
        {
            List<Admins> admins = new List<Admins>();
            string query = "select * from admins";
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                await c.OpenAsync();
                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                      
                                Admins a = new Admins
                                {
                                    admin_id = reader.GetInt32("admin_id"),
                                    first_name = reader.GetString("first_name"),
                                    last_name = reader.GetString("last_name"),
                                    username = reader.GetString("username"),
                                    password = reader.GetString("password"),
                                    is_active = reader.GetBoolean("is_active")
                                };
                                admins.Add(a);
                            }
                        
                    }
                }
                return admins;

            }
        }
        public async Task<bool> UpdateAdmin(Admins admins)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "update admins set first_name=@first_name,last_name=@last_name,username=@username,password=@password,is_active=@is_active";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@first_name", admins.first_name);
                    command.Parameters.AddWithValue("@last_name", admins.last_name);
                    command.Parameters.AddWithValue("@username", admins.username);
                    command.Parameters.AddWithValue("@password", admins.password);
                    command.Parameters.AddWithValue("@is_active", admins.is_active);
                    command.Parameters.AddWithValue("@admin_id", admins.admin_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    status = rowsAffected > 0;

                }
                catch (Exception )
                {
                    throw;
                }
            }
            return status;
        }
    }
}

