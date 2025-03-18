using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE.Services;

namespace PE.APIServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetEmployeesWithProjects()
        {
            var employees = await _employeeService.GetEmployeesWithProjectsAsync();
            return Ok(employees);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (result == null)
                return NotFound($"No employee with id {id}");

            return Ok(new
            {
                numberOfProjects = result.Value.numberOfProjects,
                numberOfSkills = result.Value.numberOfSkills,
                numberOfDepartments = result.Value.numberOfDepartments
            });
        }
    }
}
