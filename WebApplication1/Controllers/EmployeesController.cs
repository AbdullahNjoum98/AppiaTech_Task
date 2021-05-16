using Domain.Entities;
using Domain.IRepos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Employees")]
    public class EmployeesController : Controller
    {
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Regex phoneRegex = new Regex(@"^[0-9]{10}$");

        private readonly IRepository repository;

        public EmployeesController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public Task<List<Employee>> GetAllEmployees()
        {
            return repository.GetAllEmployees();
        }
        [HttpGet("{Id}")]
        public Task<Employee> GetEmployee(int Id)
        {
            return repository.GetEmployee(Id);
        }
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (repository.AddEmployee(employee))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {

            if (repository.UpdateEmployee(employee))
                return Ok();
            else
                return BadRequest();
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (repository.DeleteEmployee(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
