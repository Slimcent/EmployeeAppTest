namespace EmployeeWeb.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Models.Employee> GetAll();
        Models.Employee GetEmployee(Guid id);
        void CreateEmployee(Models.Employee employee);
    }
}
