using System.ComponentModel.DataAnnotations.Schema;

namespace Pariksha.Entities
{
    public class Quizzes
    {
        public int quiz_id {  get; set; }   
        public string description { get; set; } 
        public bool isActive { get; set; }
        public int maxMarks { get; set; }
        public int num_of_questions { get; set; }   
        public string title { get; set; }
        [ForeignKey("cat_id")]
        public int cat_id { get; set; } 

    }
}
