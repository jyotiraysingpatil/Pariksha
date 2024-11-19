using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.Entities;
using Pariksha.Services;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzeController : ControllerBase
    {
        private readonly IQuizzeService quizzeService;
        public QuizzeController(IQuizzeService quizzeService)
        {
            this.quizzeService = quizzeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuizzes([FromBody] Quizzes q)
        {
            if (q == null)
            {
                return BadRequest(new { Success = false, Message = "Invalid details" });
            }
            try
            {
                bool result = await quizzeService.AddQuizees(q);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Quizzes created successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Quizzes not inserted" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("{quiz_id}")]
        public async Task<IActionResult> UpdateQuizze(int quiz_id, [FromBody] Quizzes q)
        {
            if (q == null || quiz_id != q.quiz_id)
            {
                return BadRequest(new { Success = false, Message = "Invalid details" });
            }
            try
            {
                bool result = await quizzeService.UpdateQuizees(q);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Quiz updated successfully" });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "Quiz not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("{quiz_id}")]
        public async Task<IActionResult> DeleteQuizze(int quiz_id)
        {
            try
            {
                bool result = await quizzeService.DeleteQuizees(quiz_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "Quiz not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quizzes = await quizzeService.GetAll();
                return Ok(new { Success = true, Data = quizzes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
