using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface IQuestionRepository
    {
        public Task<List<Questions>> GetAll();
        public Task<bool> AddQuestion(Questions question);  
        public Task<bool> UpdateQuestion(Questions question);
        public Task<bool> DeleteQuestion(int ques_id);
    }
}
