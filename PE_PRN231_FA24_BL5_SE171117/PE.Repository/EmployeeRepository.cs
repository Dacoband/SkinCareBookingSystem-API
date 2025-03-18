using Microsoft.EntityFrameworkCore;
using PE.Repository.Base;
using PE.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository() { }
        public async Task<List<object>> GetEmployeesWithProjectsAsync()
        {
            var employees = await _context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    EmployeeName = e.Name,
                    Projects = e.EmployeeProjects.Select(ep => new
                    {
                        ep.Project.ProjectId,
                        ep.Project.ProjectName,
                        Role = string.IsNullOrEmpty(ep.Role) ? "No info" : ep.Role,
                        JoinDate = ep.JoinDate.HasValue ? ep.JoinDate.Value.ToString("yyyy-MM-dd") : "No info",
                        LeaveDate = ep.LeaveDate.HasValue ? ep.LeaveDate.Value.ToString("yyyy-MM-dd") : "No info"
                    }).ToList()
                })
                .ToListAsync();
            return employees.Cast<object>().ToList();
        }
        public async Task<(int numberOfProjects, int numberOfSkills, int numberOfDepartments)?> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeProjects)
                .Include(e => e.EmployeeSkills)
                .Include(e => e.Departments)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
                return null;

            int numberOfProjects = employee.EmployeeProjects.Count;
            int numberOfSkills = employee.EmployeeSkills.Count;
            int numberOfDepartments = employee.Departments.Count;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return (numberOfProjects, numberOfSkills, numberOfDepartments);
        }
    }
}
