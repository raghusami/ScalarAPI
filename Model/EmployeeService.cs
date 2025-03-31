namespace ScalarAPI.Model
{
    using ScalarAPI.Contract;
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new();

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            employee.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
            return await Task.FromResult(employee);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Position = employee.Position;
            existing.Salary = employee.Salary;
            return await Task.FromResult(existing);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return false;

            _employees.Remove(employee);
            return await Task.FromResult(true);
        }
    }

}
