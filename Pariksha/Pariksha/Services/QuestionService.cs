using Pariksha.Entities;
using Pariksha.Repository;

namespace Pariksha.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;
        public QuestionService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;   
        }
        public async Task<bool> AddQuestion(Questions question)=> await questionRepository.AddQuestion(question);   
        

        public async Task<bool> DeleteQuestion(int ques_id)=> await questionRepository.DeleteQuestion(ques_id); 
        

        public async Task<List<Questions>> GetAll()=>await questionRepository.GetAll(); 
        

        public async Task<bool> UpdateQuestion(Questions question)=>await questionRepository.UpdateQuestion(question);      
        
    }
}
