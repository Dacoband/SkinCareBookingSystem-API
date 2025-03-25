using BusinessObject.Shared.Models.BlogImages;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogImages.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImagesController : ControllerBase
    {
        private readonly ILogger<BlogImagesController> _logger;
        private static readonly List<BlogImage> blogImages = new List<BlogImage>
        {
            // Dữ liệu cứng cho bảng BlogImages
            new BlogImage
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                BlogId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                ImageUrl = "https://example.com/image1.jpg",
                Caption = "Healthy skin",
                AltText = "Skin care image",
                IsPrimary = true,
                ImageSize = 102400,
                UploadedByName = "Nguyen Van A",
                CreatedAt = DateTime.Parse("2025-03-23"),
                UpdatedAt = DateTime.Parse("2025-03-24")
            },
            new BlogImage
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                BlogId = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
                ImageUrl = "https://example.com/image2.jpg",
                Caption = "Moisturizer guide",
                AltText = "Moisturizer image",
                IsPrimary = true,
                ImageSize = 204800,
                UploadedByName = "Tran Thi B",
                CreatedAt = DateTime.Parse("2025-03-23"),
                UpdatedAt = DateTime.Parse("2025-03-23")
            }
        };

        // Constructor với dependency injection cho ILogger
        public BlogImagesController(ILogger<BlogImagesController> logger)
        {
            _logger = logger;
        }

        // GET: api/BlogImages
        // Lấy tất cả BlogImages
        [HttpGet]
        public ActionResult<IEnumerable<BlogImage>> Get()
        {
            try
            {
                _logger.LogInformation("Lấy danh sách tất cả BlogImages");
                if (!blogImages.Any())
                {
                    return NotFound("Không có BlogImage nào trong danh sách.");
                }
                return Ok(blogImages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách BlogImages");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // GET: api/BlogImages/{id}
        // Lấy BlogImage theo ID
        [HttpGet("{id}")]
        public ActionResult<BlogImage> Get(Guid id)
        {
            try
            {
                _logger.LogInformation($"Lấy BlogImage với ID: {id}");
                var blogImage = blogImages.FirstOrDefault(b => b.Id == id);
                if (blogImage == null)
                {
                    return NotFound($"Không tìm thấy BlogImage với ID: {id}");
                }
                return Ok(blogImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy BlogImage với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // POST: api/BlogImages
        // Thêm mới BlogImage
        [HttpPost]
        public ActionResult<BlogImage> Post([FromBody] BlogImage blogImage)
        {
            try
            {
                if (blogImage == null)
                {
                    return BadRequest("Dữ liệu BlogImage không hợp lệ");
                }

                // Nếu ID chưa được cung cấp, sinh ID mới
                if (blogImage.Id == Guid.Empty)
                {
                    blogImage.Id = Guid.NewGuid();
                }

                // Gán thời gian tạo và cập nhật
                blogImage.CreatedAt = DateTime.Now;
                blogImage.UpdatedAt = DateTime.Now;

                blogImages.Add(blogImage);
                _logger.LogInformation($"Đã thêm BlogImage với ID: {blogImage.Id}");

                // Trả về 201 Created với URL của resource mới
                return CreatedAtAction(nameof(Get), new { id = blogImage.Id }, blogImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm BlogImage");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // PUT: api/BlogImages/{id}
        // Cập nhật BlogImage theo ID
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] BlogImage updatedBlogImage)
        {
            try
            {
                if (updatedBlogImage == null || id != updatedBlogImage.Id)
                {
                    return BadRequest("Dữ liệu BlogImage không hợp lệ hoặc ID không khớp");
                }

                var existingBlogImage = blogImages.FirstOrDefault(b => b.Id == id);
                if (existingBlogImage == null)
                {
                    return NotFound($"Không tìm thấy BlogImage với ID: {id}");
                }

                // Cập nhật các field
                existingBlogImage.BlogId = updatedBlogImage.BlogId;
                existingBlogImage.ImageUrl = updatedBlogImage.ImageUrl;
                existingBlogImage.Caption = updatedBlogImage.Caption;
                existingBlogImage.AltText = updatedBlogImage.AltText;
                existingBlogImage.IsPrimary = updatedBlogImage.IsPrimary;
                existingBlogImage.ImageSize = updatedBlogImage.ImageSize;
                existingBlogImage.UploadedByName = updatedBlogImage.UploadedByName;
                existingBlogImage.UpdatedAt = DateTime.Now;

                _logger.LogInformation($"Đã cập nhật BlogImage với ID: {id}");
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật BlogImage với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // DELETE: api/BlogImages/{id}
        // Xóa BlogImage theo ID
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var blogImage = blogImages.FirstOrDefault(b => b.Id == id);
                if (blogImage == null)
                {
                    return NotFound($"Không tìm thấy BlogImage với ID: {id}");
                }

                blogImages.Remove(blogImage);
                _logger.LogInformation($"Đã xóa BlogImage với ID: {id}");
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa BlogImage với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }
    }
}
