using Core.Resources;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemResource>>> GetAll()
        {
            var items =await _itemService.GetAllItemsAsync();
            if (items == null)
                return NotFound("no values to return");
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemResource>> GetById(Guid id)
        {
            var item =await _itemService.GetItemByIdAsync(id);
            if(item == null)
                return NotFound("no values to return");
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemResource>> Create([FromBody] ItemResource itemResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await  _itemService.CreateItemAsync(itemResource);
            if (result == null)
                return BadRequest("not succes");
            return  CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _itemService.DeleteItemAsync(id);

            if (!success)
                return BadRequest("not succes");

            return NoContent(); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemResource>> Update(Guid id, [FromBody] ItemResource resource)
        {
            var result = await _itemService.UpdateItemAsync(id, resource);

            if (result == null)
                return NotFound("not succes");
            return Ok(result);
        }

        [HttpGet("business/{businessId}")]
        public async Task<ActionResult<IEnumerable<ItemResource>>> GetByBusiness(Guid businessId)
        {
            var result = await _itemService.GetByBusineIdAsync(businessId);
            return Ok(result);
        }


    }
}
