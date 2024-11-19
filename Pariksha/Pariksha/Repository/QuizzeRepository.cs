using MySql.Data.MySqlClient;
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public class QuizzeRepository : IQuizeRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public QuizzeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }



        public async Task<bool> AddQuizees(Quizzes quizzes)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "insert into quizzes (quiz_id,description,isActive,maxMarks, num_of_questions,title,cat_id) values (@quiz_id,@description,@isActive,@maxMarks,@num_of_questions,@title,@cat_id)";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@quiz_id", quizzes.quiz_id);
                    command.Parameters.AddWithValue("@description", quizzes.description);
                    command.Parameters.AddWithValue("@isActive", quizzes.isActive);
                    command.Parameters.AddWithValue("@maxMarks", quizzes.maxMarks);
                    command.Parameters.AddWithValue("@num_of_questions", quizzes.num_of_questions);
                    command.Parameters.AddWithValue("@title", quizzes.title);
                    command.Parameters.AddWithValue("@cat_id", quizzes.cat_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public async Task<bool> DeleteQuizees(int quiz_id)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "delete from quizzes where quiz_id=@quiz_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@quiz_id", quiz_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<List<Quizzes>> GetAll()
        {
            List<Quizzes> quizzes = new List<Quizzes>();
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                string query = "select * from quizzes";
                MySqlCommand command = new MySqlCommand(query, c);
                await c.OpenAsync();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        quizzes.Add(new Quizzes
                        {
                            quiz_id = reader.GetInt32("quiz_id"),
                            description = reader.GetString("Description"),
                            isActive = reader.GetBoolean("isActive"),
                            maxMarks = reader.GetInt32("maxMarks"),
                            num_of_questions = reader.GetInt32("num_of_questions"),
                            title = reader.GetString("title"),
                            cat_id = reader.GetInt32("cat_id")
                        });
                    }
                }
            }
            return quizzes;
        }

        public async Task<bool> UpdateQuizees(Quizzes quizzes)
        {
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "update quizzes set description=@description,isActive=@isActive,maxMarks=@maxMarks, num_of_questions=@num_of_questions,title=@title,cat_id=@cat_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@description", quizzes.description);
                    command.Parameters.AddWithValue("@isActive", quizzes.isActive);
                    command.Parameters.AddWithValue("@maxMarks", quizzes.maxMarks);
                    command.Parameters.AddWithValue("@num_of_questions", quizzes.num_of_questions);
                    command.Parameters.AddWithValue("@title", quizzes.title);
                    command.Parameters.AddWithValue("@cat_id", quizzes.cat_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
