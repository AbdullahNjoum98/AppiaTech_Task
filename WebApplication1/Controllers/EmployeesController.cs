using AutoMapper;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        
        private readonly IRepository repository;

        public EmployeesController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public Task<List<EmployeeResource>> GetAllEmployees()
        {
            return repository.GetAllEmployees();
        }
        [HttpGet("{Id}")]
        public Task<EmployeeResource> GetEmployee(int Id)
        {
            return repository.GetEmployee(Id);
        }
        [HttpPost]
        public Task<EmployeeResource> AddEmployee([FromBody] EmployeeVM employee)
        {
            repository.AddEmployee(employee);
            return repository.GetEmployee((int)employee.Id);
        }
        [HttpPut]
        public Task<EmployeeResource> UpdateEmployee([FromBody] EmployeeVM employee)
        {
            repository.UpdateEmployee(employee);
            return repository.GetEmployee((int)employee.Id);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (repository.DeleteEmployee(id)==null)
                return Ok();
            else 
            {
                string exception = HelperMethods.getException(repository.DeleteEmployee(id));
                return BadRequest(exception);
            }
        }
    }
}
