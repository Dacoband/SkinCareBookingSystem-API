using PE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Services
{
    public interface IEmployeeService
    {
        Task<List<object>> GetEmployeesWithProjectsAsync();
        Task<(int numberOfProjects, int numberOfSkills, int numberOfDepartments)?> DeleteEmployeeAsync(int employeeId);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<object>> GetEmployeesWithProjectsAsync()
        {
            return await _employeeRepository.GetEmployeesWithProjectsAsync();
        }
        public async Task<(int numberOfProjects, int numberOfSkills, int numberOfDepartments)?> DeleteEmployeeAsync(int employeeId)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }
    }
}
