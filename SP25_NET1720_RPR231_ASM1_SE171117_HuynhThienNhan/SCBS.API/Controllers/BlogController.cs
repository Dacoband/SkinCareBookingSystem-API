using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SCBS.Repositories.Models;
using SCBS.Services;

namespace SCBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableQuery]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Blog>> GetAllBlog()
        {
            return await _blogService.GetAllAsync();
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<Blog?> GetBlog(Guid id)
        {
            return await _blogService.GetByIdAsync(id);
        }
        [HttpGet("{title}/{status}")]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Blog>> SearchBlog(string title, string status)
        {
            return await _blogService.Search(title, status);
        }
        [HttpPost("create")]
        [Authorize(Roles = "1,2")]
        public async Task<int> CreateBlog(Blog blog)
        {
           return await _blogService.Create(blog);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<int> UpdateBlog(Guid id, Blog blog)
        {
            return await _blogService.Update(blog);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<bool> DeleteBlog(Guid id)
        {
            return await _blogService.Delete(id);
        }

    }
}
