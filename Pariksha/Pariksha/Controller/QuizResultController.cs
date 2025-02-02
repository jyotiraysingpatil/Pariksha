using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.Entities;
using Pariksha.Services;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService quizResultService;
        public QuizResultController(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quizResult = await quizResultService.GetAll();
                return Ok(new { Success = true, Data = quizResult });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "error " });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddQuizResult([FromBody] QuizResults quizResults)
        {
            if (quizResults == null)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            try
            {
                bool result = await quizResultService.AddQuizResult(quizResults);
                if (result)
                {
                    return CreatedAtAction(nameof(GetAll), new { quiz_res_id = quizResults.quiz_res_id }, new { Success = true, Message = "quiz results added successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "not inserted" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "an error occurred while creating the admin " });

            }
        }

        [HttpPut("{quiz_res_id}")]
        public async Task<IActionResult> UpdateQuizResult(int quiz_res_id, [FromBody] QuizResults quizResults)
        {
            if (quizResults == null || quiz_res_id != quizResults.quiz_res_id)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            try
            {
                bool result = await quizResultService.UpdateQuizResult(quizResults);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "quizresult not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }

        [HttpDelete("{quiz_res_id}")]
        public async Task<IActionResult> DeleteQuizResult(int quiz_res_id)
        {
            try
            {
                bool result = await quizResultService.DeleteQuizResult(quiz_res_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "quizResult not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }
    }
}