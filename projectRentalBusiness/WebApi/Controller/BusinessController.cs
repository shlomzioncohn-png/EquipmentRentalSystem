using Core.Resources;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessResource>>> GetAll()
        {
            var businesses = await _businessService.GetAllBusinessesAsync();

            if (businesses == null || !businesses.Any())
                return NotFound("not found");

            return Ok(businesses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessResource>> GetById(Guid id)
        {
            var business = await _businessService.GetBusinessByIdAsync(id);

            if (business == null)
                return NotFound("not found business whith id: "+id);

            return Ok(business);
        }
        

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BusinessResource>>> GetByUserId(Guid userId)
        {
            var businesses = await _businessService.GetByUserIdAsync(userId);

            if (businesses == null)
                return NotFound("not found business with user id: "+userId);

            return Ok(businesses);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessResource>> Create([FromBody] BusinessResource businessResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _businessService.CreateBusinessAsync(businessResource);

            if (result == null)
                return BadRequest("not succes");

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BusinessResource>> Update(Guid id, [FromBody] BusinessResource resource)
        {
            var result = await _businessService.UpdateBusinessAsync(id, resource);

            if (result == null)
                return NotFound("not succes");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _businessService.DeleteBusinessAsync(id);

            if (!success)
            {
                return BadRequest("Unable to delete the business, ther are items associated to this business.");
            }

            return NoContent();
        }
    }
}