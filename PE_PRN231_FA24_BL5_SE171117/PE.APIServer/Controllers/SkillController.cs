using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE.Services;

namespace PE.APIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsWithEmployeeCountAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var skill = await _skillService.GetSkillByIdWithEmployeesAsync(id);
            if (skill == null)
                return NotFound($"No skill with id {id}");

            return Ok(skill);
        }
    }
}
