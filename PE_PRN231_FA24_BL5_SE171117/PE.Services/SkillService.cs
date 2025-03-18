using PE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Services
{ 
    public interface ISkillService
    {
        Task<List<object>> GetAllSkillsWithEmployeeCountAsync();
        Task<object> GetSkillByIdWithEmployeesAsync(int skillId);
    }
    public class SkillService : ISkillService
    {
        private readonly SkillRepository _skillRepository;
        public SkillService(SkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<List<object>> GetAllSkillsWithEmployeeCountAsync()
        {
            return await _skillRepository.GetAllSkillsWithEmployeeCountAsync();
        }

        public async Task<object> GetSkillByIdWithEmployeesAsync(int skillId)
        {
            var skill = await _skillRepository.GetSkillByIdWithEmployeesAsync(skillId);
            if (skill == null)
            {
                return new { Message = $"No employee with id {skillId}" };
            }
            return skill;
        }
    }
}
