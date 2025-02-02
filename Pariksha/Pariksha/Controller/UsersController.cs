using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pariksha.DTO;
using Pariksha.Entities;
using Pariksha.Services;
using System.Linq.Expressions;

namespace Pariksha.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await userService.GetAll();
                return Ok(new { Success = true, Data = users });
            }
            catch (Exception )
            {
                return StatusCode(500, new { Success = false, Message = "an error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Users users)
        {
            if (users == null)
            {
                return BadRequest(new { Success = false, Message = "Invalid details" });
            }

            try
            {
                bool result = await userService.AddUser(users);
                if (result)
                {
                    return CreatedAtAction(nameof(GetAll), new { id = users.user_id }, new { Success = true, Message = "User created successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to create user" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An error occurred", Details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(Users users)
        {
            if (users == null)
            {
                return BadRequest(new { Success = false, Message = "invalid" });
            }
            try
            {
                bool result = await userService.UpdateUser(users);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "not found" });
                }
            }
            catch (Exception )
            {
                return StatusCode(500, new { Succes = false, Message = "internal server error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] int user_id)
        {
            try
            {
                bool result = await userService.DeleteUser(user_id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Success = false, Message = "user not found" });
                }
            }
            catch (Exception )
            {
                return StatusCode(500, new { Success = false, Message = "internal server error" });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new { Success = false, Message = "Invalid registration details" });
            }

            try
            {
                var registeredUser = await userService.RegisterUserAsync(userDto);
                if (registeredUser != null)
                {
                    return CreatedAtAction(nameof(RegisterUser), new { id = registeredUser.user_id }, new { Success = true, Data = registeredUser });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "User registration failed" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, new { Success = false, Message = "An error occurred while registering the user", Details = ex.Message });
            }
        }

        [HttpGet("id/{user_id}")]
        public async Task<IActionResult> GetUserById(int user_id)
        {
            try
            {
                var user = await userService.FindUserById(user_id);
                if (user != null)
                {
                    return Ok(new { Success = true, Data = user });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An error occurred while finding the user", Details = ex.Message });
            }
        }


        [HttpGet("name/{firstName}")]
        public async Task<IActionResult> findByFirstName(string firstName)
        {
            try
            {
                var user = await userService.FindByFirstName(firstName);
                if (user != null)
                {
                    return Ok(new { Success = true, Data = user });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An error occurred while finding the user", Details = ex.Message });
            }
        }

    }

}