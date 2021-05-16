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
        public IActionResult AddEmployee([FromBody] EmployeeVM employee)
        {
            if (repository.AddEmployee(employee))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeVM employee)
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
