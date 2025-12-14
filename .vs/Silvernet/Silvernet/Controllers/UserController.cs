using Microsoft.AspNetCore.Mvc;
using Silvernet.DTOs;
using Silvernet.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Silvernet.Controllers
{
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetByTenantId (long tenantId)
        {
            try
            {
                var users = await _userService.GetUsersByTenantId(tenantId);

                if (users == null)
                {
                    return NotFound($"no users for tenant: {tenantId} ");
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
