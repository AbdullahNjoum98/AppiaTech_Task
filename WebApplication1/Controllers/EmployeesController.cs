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
            var Id = repository.AddEmployee(employee);
            if (Id != 0)
                return repository.GetEmployee((int)employee.Id);
            else
                return null;
        }
        [HttpPut]
        public Task<EmployeeResource> UpdateEmployee([FromBody] EmployeeVM employee)
        {
            var exception = repository.UpdateEmployee(employee);
            if (exception == null)
                return repository.GetEmployee((int)employee.Id);
            else
                return null;
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var exception = repository.DeleteEmployee(id);
            if (exception == null)
                return Ok();
            else 
            {
                return BadRequest(HelperMethods.getException(exception));
            }
        }
    }
}
