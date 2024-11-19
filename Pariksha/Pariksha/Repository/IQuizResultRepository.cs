using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface IQuizResultRepository
    {
        public Task<List<QuizResults>> GetAll();
        public Task<bool> AddQuizResult(QuizResults result);
        public Task<bool> DeleteQuizResult(int quiz_res_id);
        public Task<bool> UpdateQuizResult(QuizResults result);
    }
}
