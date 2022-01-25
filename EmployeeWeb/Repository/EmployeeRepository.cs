using EmployeeWeb.Interfaces;
using EmployeeWeb.Models.Context;

namespace EmployeeWeb.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Employee> GetAll() => _context.Employees.ToList();

        public Models.Employee GetEmployee(Guid id) => _context.Employees.SingleOrDefault(e => e.Id.Equals(id));

        public void CreateEmployee(Models.Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }
    }
}
