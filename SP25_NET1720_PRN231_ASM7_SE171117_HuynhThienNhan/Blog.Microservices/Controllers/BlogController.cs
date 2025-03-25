
using BusinessObject.Shared.Models.Blogs;
using Common.Shared;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroBlogs.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger _logger;
        private static readonly List<Blog> blogs = new List<Blog>
        {
            // Dữ liệu cứng cho bảng Blog
            new Blog
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Top 5 Skin Care Tips",
                Description = "Tips to keep your skin healthy.",
                Status = "Published",
                AuthorName = "Nguyen Van A",
                PublishDate = DateTime.Parse("2025-03-24"),
                Views = 150,
                Tags = "skincare, beauty",
                IsFeatured = true,
                CreatedAt = DateTime.Parse("2025-03-23"),
                UpdatedAt = DateTime.Parse("2025-03-24")
            },
            new Blog
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "How to Choose Moisturizer",
                Description = "Guide to pick the right product.",
                Status = "Draft",
                AuthorName = "Tran Thi B",
                PublishDate = null,
                Views = 0,
                Tags = "moisturizer",
                IsFeatured = false,
                CreatedAt = DateTime.Parse("2025-03-23"),
                UpdatedAt = DateTime.Parse("2025-03-23")
            }
        };
        private readonly IBus _bus;
        // Constructor với dependency injection cho ILogger
        public BlogController(ILogger<Blog> logger, IBus iBus)
        {
            _logger = logger;
            _bus = iBus;
        }

        // GET: api/Blog
        // Lấy tất cả Blog
        [HttpGet]
        public ActionResult<IEnumerable<Blog>> Get()
        {
            try
            {
                _logger.LogInformation("Lấy danh sách tất cả Blog");
                if (!blogs.Any())
                {
                    return NotFound("Không có Blog nào trong danh sách.");
                }
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách Blog");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // GET: api/Blog/{id}
        // Lấy Blog theo ID
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(Guid id)
        {
            try
            {
                _logger.LogInformation($"Lấy Blog với ID: {id}");
                var blog = blogs.FirstOrDefault(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound($"Không tìm thấy Blog với ID: {id}");
                }
                return Ok(blog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy Blog với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // POST: api/Blog
        // Thêm mới Blog
        [HttpPost]
        public async Task<IActionResult> Post(Blog blog)
        {
            if (blog != null)
            {
                #region Business rule process anh/or call other API Service

                #endregion

                #region Publish data to Queue on RabbitMQ

                //Lets Queue as orderQueue.
                //Create a new URL ‘rabbitmq://localhost/orderQueue’
                //If orderQueue does not exist, RabbitMQ creates one
                Uri uri = new Uri("rabbitmq://localhost/orderQueue");
                //Gets an endpoint to send the shared model object
                var endPoint = await _bus.GetSendEndpoint(uri);
                //Push the message to the queue
                await endPoint.Send(blog);

                #endregion

                #region Logger

                string messageLog = string.Format("[{0}] PUBLISH data to RabbitMQ.orderQueue: {1}", DateTime.Now, JsonUtils.ConvertObjectToJSONString(blog));
                JsonUtils.WriteLoggerFile(messageLog);
                _logger.LogInformation(messageLog);

                #endregion


                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/Blog/{id}
        // Cập nhật Blog theo ID
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Blog updatedBlog)
        {
            try
            {
                if (updatedBlog == null || id != updatedBlog.Id)
                {
                    return BadRequest("Dữ liệu Blog không hợp lệ hoặc ID không khớp");
                }

                var existingBlog = blogs.FirstOrDefault(b => b.Id == id);
                if (existingBlog == null)
                {
                    return NotFound($"Không tìm thấy Blog với ID: {id}");
                }

                // Đảm bảo Status hợp lệ
                if (updatedBlog.Status != "Draft" && updatedBlog.Status != "Published")
                {
                    return BadRequest("Status phải là 'Draft' hoặc 'Published'");
                }

                // Cập nhật các field
                existingBlog.Title = updatedBlog.Title;
                existingBlog.Description = updatedBlog.Description;
                existingBlog.Status = updatedBlog.Status;
                existingBlog.AuthorName = updatedBlog.AuthorName;
                existingBlog.PublishDate = updatedBlog.PublishDate;
                existingBlog.Views = updatedBlog.Views;
                existingBlog.Tags = updatedBlog.Tags;
                existingBlog.IsFeatured = updatedBlog.IsFeatured;
                existingBlog.UpdatedAt = DateTime.Now;

                _logger.LogInformation($"Đã cập nhật Blog với ID: {id}");
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật Blog với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }

        // DELETE: api/Blog/{id}
        // Xóa Blog theo ID
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var blog = blogs.FirstOrDefault(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound($"Không tìm thấy Blog với ID: {id}");
                }

                blogs.Remove(blog);
                _logger.LogInformation($"Đã xóa Blog với ID: {id}");
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa Blog với ID: {id}");
                return StatusCode(500, "Lỗi máy chủ nội bộ");
            }
        }
    }
}
