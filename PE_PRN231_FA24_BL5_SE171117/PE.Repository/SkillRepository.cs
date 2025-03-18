using Microsoft.EntityFrameworkCore;
using PE.Repository.Base;
using PE.Repository.DBContext;
using PE.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Repository
{
    public class SkillRepository : GenericRepository<Skill>
    {   
        public SkillRepository() : base()
        {
        }
        
        public async Task<List<object>> GetAllSkillsWithEmployeeCountAsync()
        {
            var skills = await _context.Skills
                .Select(skill => new
                {
                    skill.SkillId,
                    skill.SkillName,
                    skill.Description,
                    NumberOfEmployee = skill.EmployeeSkills.Count
                })
                .ToListAsync();

            return skills.Cast<object>().ToList();
        }

        public async Task<object> GetSkillByIdWithEmployeesAsync(int skillId)
        {
            var skill = await _context.Skills
                .Where(s => s.SkillId == skillId)
                .Select(s => new
                {
                    s.SkillId,
                    s.SkillName,
                    s.Description,
                    Employees = s.EmployeeSkills.Select(es => new
                    {
                        es.Employee.EmployeeId,
                        EmployeeName = es.Employee.Name,
                        Department = es.Employee.Department.DepartmentName,
                        es.ProficiencyLevel,
                        es.AcquiredDate
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return skill;
        }
    }
}
