using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SCBS.Repositories.Models;
using SCBS.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableQuery]
    public class BlogImageController : ControllerBase
    {
        private readonly IBlogImageService _blogImageService;
        public BlogImageController(IBlogImageService blogImageService)
        {
            _blogImageService = blogImageService;
        }
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<BlogImage>> GetAllBlogImage()
        {
            return await _blogImageService.GetAllAsync();
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<BlogImage?> GetBlogImage(Guid id)
        {
            return await _blogImageService.GetByIdAsync(id);
        }
        [HttpGet("{blogId}")]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<BlogImage>> SearchBlogImage(Guid blogId)
        {
            return await _blogImageService.Search(blogId);
        }
        [HttpPost("create")]
        [Authorize(Roles = "1,2")]
        public async Task<int> CreateBlogImage(BlogImage blogImage)
        {
            return await _blogImageService.Create(blogImage);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<int> UpdateBlogImage(Guid id, BlogImage blogImage)
        {
            return await _blogImageService.Update(blogImage);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<bool> DeleteBlogImage(Guid id)
        {
            return await _blogImageService.Delete(id);
        }
    }
}
