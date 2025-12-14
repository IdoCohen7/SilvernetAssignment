using Microsoft.AspNetCore.Mvc;
using Silvernet.DTOs;
using Silvernet.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Silvernet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {   
        private readonly ITenantService _tenantService;
        private readonly ILogger<TenantController> _logger;


        public TenantController(ITenantService tenantService, ILogger<TenantController> logger)
        {
            _tenantService = tenantService;
            _logger = logger;

        }
        
        // GET: api/<TenantController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> Get()
        {
            try
            {
                var tenants = await _tenantService.GetAllTenants();

                if (tenants == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Fetched all tenants successsfully!");
                return Ok(tenants);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Interanl Server Error");
            }
        }

        // GET api/<TenantController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDTO>> Get(long id)
        {
            try
            {
                var tenant = await _tenantService.GetTenantByIdAsync(id);
                if (tenant == null)
                {
                    return NotFound("Id mismatch for tenant");
                }

                return Ok(tenant);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }
            
        }

        // POST api/<TenantController>
        [HttpPost]
        public async Task<ActionResult<TenantDTO>> Post([FromBody] TenantCreateDTO tenantDTO)
        {
            _logger.LogInformation("Attempting to create tenant with email: {Email}", tenantDTO.Email);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTenant = await _tenantService.CreateTenantAsync(tenantDTO);
                _logger.LogInformation("Tenant {Id} created successfully.", tenantDTO.Email);

                return CreatedAtAction(nameof(Get), new { id = newTenant.Id }, newTenant);

            }

            catch (Exception ex)
            {
                return StatusCode(500, "Server Error");
            }

        }

        // PUT api/<TenantController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TenantUpdateDTO tenantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool status = await _tenantService.UpdateTenantAsync(id, tenantDTO);

                if (!status)
                {
                    _logger.LogWarning("Attempt to update non-existing tenant ID: {Id}", id);
                    return NotFound($"tenant with ID {id} not found.");
                }
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<TenantController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool success = await _tenantService.DeleteTenantAsync(id);

                if (!success)
                {
                    _logger.LogWarning("Attempt to delete non-existing tenant ID: {Id}", id);
                    return NotFound($"tenant with ID {id} not found.");
                }

                _logger.LogInformation("Tenant {Id} deleted successfully.", id);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
