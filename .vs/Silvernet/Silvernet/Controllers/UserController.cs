using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silvernet.DTOs;
using Silvernet.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Silvernet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;


        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService; 
            _logger = logger;

        }
        
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                if (users == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Fetched all users successfully!");
                return Ok(users);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<UserController>/tenant/5
        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetByTenantId(long tenantId)
        {
            try
            {
                var users = await _userService.GetUsersByTenantId(tenantId);

                if (users == null)
                {
                    return NotFound($"No users for tenant: {tenantId}");
                }

                return Ok(users);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(long id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found");
                }

                return Ok(user);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserCreateDTO userDTO)
        {
            _logger.LogInformation("Attempting to create user with email: {Email}", userDTO.Email);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUser = await _userService.CreateUserAsync(userDTO);
                _logger.LogInformation("User {Id} created successfully.", newUser.Id);

                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);

            }

            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UserUpdateDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool status = await _userService.UpdateUserAsync(id, userDTO);

                if (!status)
                {
                    _logger.LogWarning("Attempt to update non-existing user ID: {Id}", id);
                    return NotFound($"User with ID {id} not found.");
                }
                
                _logger.LogInformation("User {Id} updated successfully.", id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                bool success = await _userService.DeleteUserAsync(id);

                if (!success)
                {
                    _logger.LogWarning("Attempt to delete non-existing user ID: {Id}", id);
                    return NotFound($"User with ID {id} not found.");
                }

                _logger.LogInformation("User {Id} deleted successfully.", id);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
