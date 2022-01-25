using EmployeeWeb.Interfaces;
using EmployeeWeb.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repo;
        private readonly AccountNumberValidation _validation;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
            _validation = new AccountNumberValidation();
        }

        public IActionResult Index()
        {
            var employees = _repo.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,AccountNumber,Age")] Models.Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            if (!_validation.IsValid(employee.AccountNumber))
            {
                ModelState.AddModelError("AccountNumber", "Account Number is Invald");
                return View(employee);
            }

            _repo.CreateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
