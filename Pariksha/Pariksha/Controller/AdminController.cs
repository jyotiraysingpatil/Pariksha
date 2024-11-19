using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.Entities;
using Pariksha.Services;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var admins = await adminService.GetAll();
                return Ok(new { Success = true, Data = admins });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "error " });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] Admins admin)
        {
            if (admin == null)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            try
            {
                bool result = await adminService.AddAdmin(admin);
                if (result)
                {
                    return CreatedAtAction(nameof(GetAll), new { admin_id = admin.admin_id }, new { Success = true, Message = "admin created successfully" });
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

        [HttpPut("{admin_id}")]
        public async Task<IActionResult> UpdateAdmin(int admin_id, [FromBody] Admins admin)
        {
            if (admin == null || admin_id != admin.admin_id)
            {
                return BadRequest(new { Success = false, Message = "invalid details" });
            }
            try
            {
                bool result = await adminService.UpdateAdmin(admin);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "admin not found" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }

        [HttpDelete("{admin_id}")]
        public async Task<IActionResult> DeleteAdmin(int admin_id)
        {
            try
            {
                bool result = await adminService.DeleteAdmin(admin_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "admin not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }
    }
}