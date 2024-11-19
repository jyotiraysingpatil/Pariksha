using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.Entities;
using Pariksha.Services;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await categoryService.GetAll();
                return Ok(new { Success = true, Data = categories });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "error " });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Categories c)
        {
            if (c == null)
            {
                return BadRequest(new { Success = false, Message = "Invalid details" });
            }

            try
            {
                bool result = await categoryService.AddCategory(c);
                if (result)
                {
                    return CreatedAtAction(nameof(GetAll), new { cat_id = c.cat_id }, new { Success = true, Message = "Category created successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Category not inserted" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("{cat_id}")]
        public async Task<IActionResult> UpdateCategory(int cat_id, [FromBody] Categories c)
        {
            if (c == null || cat_id != c.cat_id)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            try
            {
                bool result = await categoryService.UpdateCategory(c);
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }

        [HttpDelete("{cat_id}")]
        public async Task<IActionResult> DeleteCategory(int cat_id)
        {
            try
            {
                bool result = await categoryService.DeleteCategory(cat_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "category not found" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }

        }
    }
}