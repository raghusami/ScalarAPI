using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ScalarAPI.Contract;
using ScalarAPI.Model;

namespace ScalarAPI.Controllers
{
    [Authorize]

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "EmployeeCache";

        public EmployeeController(IEmployeeService employeeService, IMemoryCache cache)
        {
            _employeeService = employeeService;
            _cache = cache;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            if (!_cache.TryGetValue(CacheKey, out List<Employee> employees))
            {
                employees = await _employeeService.GetEmployeesAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(CacheKey, employees, cacheOptions);
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var newEmployee = await _employeeService.AddEmployeeAsync(employee);

            // Clear cache to refresh data
            _cache.Remove(CacheKey);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
            if (updatedEmployee == null) return NotFound();

            // Clear cache to refresh data
            _cache.Remove(CacheKey);

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success) return NotFound();

            // Clear cache to refresh data
            _cache.Remove(CacheKey);

            return NoContent();
        }
    }

}
