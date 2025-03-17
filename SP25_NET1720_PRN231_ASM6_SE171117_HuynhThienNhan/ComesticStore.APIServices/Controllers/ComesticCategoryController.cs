using ComesticStore.Repositories;
using ComesticStore.Repositories.Models;
using ComesticStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComesticStore.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComesticCategoryController : ControllerBase
    {
        private readonly IComesticCategoryService _comesticCategoryService;
        public ComesticCategoryController(IComesticCategoryService comesticCategoryService)
        {
            _comesticCategoryService = comesticCategoryService;
        }
        [HttpGet]
        public async Task<IEnumerable<CosmeticCategory>> Get()
         => await _comesticCategoryService.GetAllAsync();
    }
}
