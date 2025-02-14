using Microsoft.AspNetCore.Mvc;
using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem_Service;

namespace SchoolPsychologicalHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _cateService;
        public CategoryController(ICategoryService cateService)
        {
            _cateService = cateService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var item = await _cateService.GetAll();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var item = await _cateService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Category item)
        {
            var createdId = await _cateService.Create(item);
            return CreatedAtAction(nameof(Get), new { id = createdId }, createdId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] Category item)
        {
            if (id != item.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedId = await _cateService.Update(item);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _cateService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Category>>> Search([FromQuery] string name)
        {
            var list  = await _cateService.Search(name);
            return Ok(list);
        }
    }
}
