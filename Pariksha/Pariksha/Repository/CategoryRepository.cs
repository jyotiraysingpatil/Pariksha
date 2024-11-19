using MySql.Data.MySqlClient;
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public CategoryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<bool> AddCategory(Categories category)
        {
            using (var c = new MySqlConnection(_connectionString))
            {
                await c.OpenAsync();

                // Check if Admin ID exists
                string adminCheckQuery = "SELECT COUNT(1) FROM admins WHERE admin_id = @admin_id";
                MySqlCommand adminCommand = new MySqlCommand(adminCheckQuery, c);
                adminCommand.Parameters.AddWithValue("@admin_id", category.admin_id);

                int adminExists = Convert.ToInt32(await adminCommand.ExecuteScalarAsync());

                if (adminExists == 0)
                {
                    throw new Exception($"Admin ID {category.admin_id} does not exist. Please use a valid Admin ID.");
                }

                // Insert Category
                string query = "INSERT INTO categories (description, title, admin_id) VALUES (@description, @title, @admin_id)";
                MySqlCommand command = new MySqlCommand(query, c);
                command.Parameters.AddWithValue("@description", category.description);
                command.Parameters.AddWithValue("@title", category.title);
                command.Parameters.AddWithValue("@admin_id", category.admin_id);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
        


        public async Task<bool> DeleteCategory(int cat_id)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "delete from categories where cat_id=@cat_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@cat_id", cat_id);
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


        public async Task<List<Categories>> GetAll()
        {
            List<Categories> list = new List<Categories>();
            string query = "select * from categories";
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                await c.OpenAsync();
                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categories categories = new Categories()
                            {
                                cat_id = reader.GetInt32("cat_id"),
                                description = reader.GetString("description"),
                                title = reader.GetString("title"),
                                admin_id = reader.GetInt32("admin_id")
                            };
                            list.Add(categories);
                        }
                    }
                }
                return list;
            }
        }

        public async Task<bool> UpdateCategory(Categories category)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE categories SET description = @description, title = @title, admin_id = @admin_id WHERE cat_id = @cat_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@description", category.description);
                    command.Parameters.AddWithValue("@title", category.title);
                    command.Parameters.AddWithValue("@admin_id", category.admin_id);
                    command.Parameters.AddWithValue("@cat_id", category.cat_id);  // Corrected parameter name

                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    status = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the category.", ex);
                }
            }
            return status;
        }

    }
}