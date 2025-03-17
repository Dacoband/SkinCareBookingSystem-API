using ComesticStore.Repositories.Models;
using ComesticStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace ComesticStore.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableQuery]
    public class ComesticInformationController : ControllerBase
    {
        private readonly IComesticInformationService _comesticInformationService;
        public ComesticInformationController(IComesticInformationService comesticInformationService)
        {
            _comesticInformationService = comesticInformationService;
        }
        [HttpGet]
        [Authorize(Roles = "1,3,4")]
        public async Task<IEnumerable<CosmeticInformation>> Get()
         => await _comesticInformationService.GetAllAsync();

        // GET api/<CosmeticInformationController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public async Task<CosmeticInformation> Get(string id) => await _comesticInformationService.GetByIdAsync(id);

        // POST api/<CosmeticInformationController>
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<int> Post([FromBody] CosmeticInformation value) => await _comesticInformationService.CreateAsync(value);

        // PUT api/<CosmeticInformationController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<int> Put(string id, [FromBody] CosmeticInformation value) => await _comesticInformationService.UpdateAsync(value);

        // DELETE api/<CosmeticInformationController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<bool> Delete(string id) => await _comesticInformationService.RemoveAsync(id);
    }
}
