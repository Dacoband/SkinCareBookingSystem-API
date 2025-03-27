using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCBS.Repositories.Models;
using SCBS.Services;

namespace SCBS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImageController : ControllerBase
    {
        private readonly IBlogImageService _blogImageService;
        public BlogImageController(IBlogImageService blogImageService) => _blogImageService = blogImageService;
       
        [HttpGet]
        public async Task<IEnumerable<BlogImage>> Get() => await _blogImageService.GetAllAsync();

       
        [HttpGet("{id}")]
        public async Task<BlogImage> Get(Guid id) => await _blogImageService.GetByIdAsync(id);
        [HttpPost]
        public async Task<int> Post([FromBody] BlogImage blogImage) => await _blogImageService.CreateAsync(blogImage);
        [HttpPut]
        public async Task<int> Put([FromBody] BlogImage blogImage) => await _blogImageService.UpdateAsync(blogImage);
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id) => await _blogImageService.DeleteAsync(id);
    }
}
