using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.Entities;
using Pariksha.Services;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var questions = await questionService.GetAll();
                return Ok(new { Success = true, Data = questions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Questions q)
        {
            if (q == null)
            {
                return BadRequest(new { Success = false, Message = "Invalid details" });

            }
            try
            {
                bool result = await questionService.AddQuestion(q);
                if (result)
                {
                    return CreatedAtAction(nameof(GetAll), new { ques_id = q.ques_id }, new { Success = true, Message = "question created successfully" });
                }
                else
                {
                    return StatusCode(500, new { Success = false, Message = "Question not inserted" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"An error occurred:{ex.Message}" });
            }
        }

        [HttpPut("{ques_id}")]
        public async Task<IActionResult> UpdateQuestion(int ques_id, [FromBody] Questions q)
        {
            if (q == null || ques_id != ques_id)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });

            }
            try
            {
                bool result = await questionService.UpdateQuestion(q);
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }

        [HttpDelete("{ques_id}")]
        public async Task<IActionResult> DeleteQuestion(int ques_id)
        {
            try
            {
bool result=await questionService.DeleteQuestion(ques_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "question not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }
    }
}