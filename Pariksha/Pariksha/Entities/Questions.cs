using System.ComponentModel.DataAnnotations.Schema;

namespace Pariksha.Entities
{
    public class Questions
    {
        public int ques_id {  get; set; }   
        public string answer { get; set; }  
        public string question { get; set; }    
        public string option1 { get; set; } 
        public string option2 { get; set; } 
        public string option3 { get; set; }
        public string option4 { get; set; }
       
        [ForeignKey("quiz_id")]
        public int quiz_id { get; set; }

    }
}
