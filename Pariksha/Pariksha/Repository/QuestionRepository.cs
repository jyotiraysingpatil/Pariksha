using MySql.Data.MySqlClient;
using Pariksha.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Ubiety.Dns.Core;
namespace Pariksha.Repository
{
    public class QuestionRepository:IQuestionRepository
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public QuestionRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> AddQuestion(Questions question)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query="Insert Into questions (ques_id,answer,question,option1,option2,option3,option4,quiz_id) VALUES (@ques_id,@answer,@question,@option1,@option2,@option3,@option4,@quiz_id)";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@ques_id", question.ques_id);
                    command.Parameters.AddWithValue("@answer", question.answer);
                    command.Parameters.AddWithValue("@question", question.question);
                    command.Parameters.AddWithValue("@option1", question.option1);
                    command.Parameters.AddWithValue("@option2", question.option2);
                    command.Parameters.AddWithValue("@option3", question.option3);
                    command.Parameters.AddWithValue("@option4", question.option4);
                    command.Parameters.AddWithValue("@quiz_id", question.quiz_id);
                    await c.OpenAsync();
                    int rowsAffected= await command.ExecuteNonQueryAsync();
                    status= rowsAffected > 0;


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred:{ex.Message}");
                    throw;
                }

            }
            return status;
        }

        public async Task<bool> DeleteQuestion(int ques_id)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "delete from questions where ques_id=@ques_id";
                    MySqlCommand command = new MySqlCommand(query, c);
                    command.Parameters.AddWithValue("@ques_id", ques_id);
                    await c.OpenAsync();
                    int rowsAffected=await command.ExecuteNonQueryAsync();
                    status= rowsAffected > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return status;
        }


        public async Task<List<Questions>> GetAll()
        {
            List<Questions> questions = new List<Questions>();
            string query = "select * from questions";
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                await c.OpenAsync();
                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Questions q = new Questions()
                            {
                                ques_id = reader.GetInt32("ques_id"),
                                answer = reader.GetString("answer"),
                                question = reader.GetString("question"),
                                option1 = reader.GetString("option1"),
                                option2 = reader.GetString("option2"),
                                option3 = reader.GetString("option3"),
                                option4 = reader.GetString("option4"),
                                quiz_id = reader.GetInt32("quiz_id")

                            };
                            questions.Add(q);
                        }
                    }
                }
            }
            return questions;
        }
        public async Task<bool> UpdateQuestion(Questions question)
        {
            bool status = false;
            using (MySqlConnection c = new MySqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE questions SET answer = @answer, question = @question, option1 = @option1, " +
                                   "option2 = @option2, option3 = @option3, option4 = @option4, quiz_id = @quiz_id " +
                                   "WHERE ques_id = @ques_id";

                    MySqlCommand command = new MySqlCommand(query, c);
                   
                    command.Parameters.AddWithValue("@answer", question.answer);
                    command.Parameters.AddWithValue("@question", question.question);
                    command.Parameters.AddWithValue("@option1", question.option1);
                    command.Parameters.AddWithValue("@option2", question.option2);
                    command.Parameters.AddWithValue("@option3", question.option3);
                    command.Parameters.AddWithValue("@option4", question.option4);
                    command.Parameters.AddWithValue("@quiz_id", question.quiz_id);
                    await c.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    status = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred:{ex.Message}");
                    throw;
                }

            }
            return status;
        
        }
    }
}
