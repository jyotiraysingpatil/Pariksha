
using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface IQuizeRepository
    {
        Task<bool> AddQuizees(Quizzes quizzes);
        Task<bool> UpdateQuizees(Quizzes quizzes);
        Task<List<Quizzes>> GetAll();
        Task<bool> DeleteQuizees(int quiz_id);

    }
}
