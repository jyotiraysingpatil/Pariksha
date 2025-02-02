using Pariksha.Entities;
using Pariksha.Repository;

namespace Pariksha.Services
{
    public class QuizResultService:IQuizResultService
    {
        private readonly IQuizResultRepository quizResultRepository;
        public QuizResultService(IQuizResultRepository quizResultRepository)
        {
            this.quizResultRepository = quizResultRepository;
        }

        public async  Task<bool> AddQuizResult(QuizResults result)=> await quizResultRepository.AddQuizResult(result);  
        

        public async Task<bool> DeleteQuizResult(int quiz_res_id)=>await quizResultRepository.DeleteQuizResult(quiz_res_id);    
        

        public async Task<List<QuizResults>> GetAll()=> await quizResultRepository.GetAll();    
        

        public async Task<bool> UpdateQuizResult(QuizResults result)=> await quizResultRepository.UpdateQuizResult(result); 
       
    }
}
