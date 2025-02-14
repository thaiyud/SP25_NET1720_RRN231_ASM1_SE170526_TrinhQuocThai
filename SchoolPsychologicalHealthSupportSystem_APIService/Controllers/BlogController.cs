using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem_Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPsychologicalHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Blog>>> Get()
        {
            var blogs = await _blogService.GetAll();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> Get(int id)
        {
            var blog = await _blogService.GetById(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Blog blog)
        {
            var createdId = await _blogService.Create(blog);
            return CreatedAtAction(nameof(Get), new { id = createdId }, createdId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedId = await _blogService.Update(blog);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _blogService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Blog>>> Search([FromQuery] string name, [FromQuery] string title, [FromQuery] string cateName)
        {
            var blogs = await _blogService.Search(name, title, cateName);
            return Ok(blogs);
        }
    }
}
