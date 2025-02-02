using Pariksha.Entities;
using Pariksha.Repository;

namespace Pariksha.Services
{
    public class QuizzeService : IQuizzeService
    {
        private readonly IQuizeRepository _repository;
        public QuizzeService(IQuizeRepository repository)
        {
            _repository = repository;
        }



        public async Task<bool> AddQuizees(Quizzes quizzes)=> await _repository.AddQuizees(quizzes);
        

        public async Task<bool> DeleteQuizees(int quiz_id)=> await _repository.DeleteQuizees(quiz_id);  
        

        public async Task<List<Quizzes>> GetAll()=> await _repository.GetAll();
        

        public async Task<bool> UpdateQuizees(Quizzes quizzes)=>await _repository.UpdateQuizees(quizzes);   
        
       


    }
}
