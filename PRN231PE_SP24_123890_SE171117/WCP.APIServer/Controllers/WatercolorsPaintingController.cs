using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WCP.Repositories.Models;
using WCP.Services;

namespace WCP.APIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableQuery]
    public class WatercolorsPaintingController : ControllerBase
    {
        private readonly IWatercolorsPaintingService _watercolorsPaintingService;
        private readonly IStyleService _styleService;
        public WatercolorsPaintingController(IWatercolorsPaintingService watercolorsPaintingService, IStyleService styleService)
        {
            _watercolorsPaintingService = watercolorsPaintingService;
            _styleService = styleService;
        }

        [HttpGet]
        [Authorize(Roles = "2,3")]
        public async Task<IEnumerable<WatercolorsPainting>> Get()
         => await _watercolorsPaintingService.GetAllAsync();

        [HttpGet("{id}")]
        [Authorize(Roles = "3")]
        public async Task<WatercolorsPainting> Get(string id)
         => await _watercolorsPaintingService.GetByIdAsync(id);

        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<int> Post([FromBody] WatercolorsPainting watercolorsPainting)
         => await _watercolorsPaintingService.CreateAsync(watercolorsPainting);

        [HttpPut("{id}")]
        [Authorize(Roles = "3")]
        public async Task<int> Put(string id, [FromBody] WatercolorsPainting watercolorsPainting)
         => await _watercolorsPaintingService.UpdateAsync(watercolorsPainting);

        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<bool> Delete(string id)
         => await _watercolorsPaintingService.RemoveAsync(id);

        [HttpGet("styles")]
        [Authorize(Roles = "3")]
        public async Task<IEnumerable<Style>> GetStyles()
         => await _styleService.GetAllAsync();
    }
}
