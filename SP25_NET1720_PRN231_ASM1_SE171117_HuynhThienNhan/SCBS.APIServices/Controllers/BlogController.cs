using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SCBS.Repositories.Models;
using SCBS.Services;

namespace SCBS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableQuery]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) => _blogService = blogService;
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Blog>> Get() => await _blogService.GetAllAsync();
        //[HttpGet("search")]
        //[Authorize(Roles = "1,2")]
        //public async Task<IEnumerable<Blog>> Search([FromQuery] string? title, [FromQuery] string? location, [FromQuery] string? status) => await _blogService.Search(title, location, status);
        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<Blog> Get(Guid id) => await _blogService.GetByIdAsync(id);

        // POST api/<ScheduleController>
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<int> Post([FromBody] Blog value) => await _blogService.CreateAsync(value);

        // PUT api/<ScheduleController>/5
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<int> Put([FromBody] Blog value) => await _blogService.UpdateAsync(value);

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<bool> Delete(Guid id) => await _blogService.RemoveAsync(id);
    }
}
