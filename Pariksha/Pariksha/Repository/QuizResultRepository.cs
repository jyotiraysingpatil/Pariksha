using Microsoft.EntityFrameworkCore.Diagnostics;
using MySql.Data.MySqlClient;
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public QuizResultRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }





        public async Task<bool> AddQuizResult(QuizResults result)
        {
            bool status= false;
            using(MySqlConnection c=new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "Insert into quizResults (quiz_res_id,attemptDateTime,totalObtainedMarks,quiz_id,user_id) values (@quiz_res_id,@attemptDateTime,@totalObtainedMarks,@quiz_id,@user_id)";
                    MySqlCommand command= new MySqlCommand(query,c);
                    command.Parameters.AddWithValue("quiz_res_id", result.quiz_res_id);
                    command.Parameters.AddWithValue("attemptDateTime", result.attemptDateTime);
                    command.Parameters.AddWithValue("totalObtainedMarks", result.totalObtainedMarks);
                    command.Parameters.AddWithValue("quiz_id", result.quiz_id);
                    command.Parameters.AddWithValue("user_id",result.user_id);  
                    await c.OpenAsync();    
                    int rowsAffected=await command.ExecuteNonQueryAsync();  
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

        public async Task<bool> DeleteQuizResult(int quiz_res_id)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                {
                    try
                    {
                        string query = "delete from quizResults where quiz_res_id=@quiz_res_id";
                        MySqlCommand command = new MySqlCommand(query, c);
                        command.Parameters.AddWithValue("@quiz_res_id", quiz_res_id);
                        await c.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        status = rowsAffected > 0;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return status;
            }
        }

        public async Task<List<QuizResults>> GetAll()
        {
            List<QuizResults> results = new List<QuizResults>();
            string query = "select * from quizResults";
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                await c.OpenAsync();
                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizResults q = new QuizResults()
                            {
                                quiz_res_id = reader.GetInt32("quiz_res_id"),
                                attemptDateTime = reader.GetDateTime("attemptDateTime"),
                                totalObtainedMarks = reader.GetInt32("totalObtainedMarks"),
                                quiz_id = reader.GetInt32("quiz_id"),
                                user_id = reader.GetInt32("user_id")

                            };
                            results.Add(q);
                        }
                    }

                }
                return results;
            }
        }


        public async Task<bool> UpdateQuizResult(QuizResults result)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE quizResults SET attemptDateTime=@attemptDateTime, totalObtainedMarks=@totalObtainedMarks WHERE quiz_res_id=@quiz_res_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@attemptDateTime", result.attemptDateTime);
                    command.Parameters.AddWithValue("@totalObtainedMarks", result.totalObtainedMarks);
                    command.Parameters.AddWithValue("@quiz_res_id", result.quiz_res_id);

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

    }
}

