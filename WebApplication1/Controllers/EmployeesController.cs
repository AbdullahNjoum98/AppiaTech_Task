using Domain.IRepos;
using Cotracts.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace TaskAPI.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        
        private readonly IManager manager;

        public EmployeesController(IManager manager)
        {
            this.manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emps = await manager.GetAllEmployees();
            return Ok(emps);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployee(int Id)
        {
            var emp = await manager.GetEmployee(Id);
            return Ok(emp);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeVM employee)
        {
            var emp = await manager.AddEmployee(employee);
            return Ok(emp);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeVM employee)
        {
            var emp = await manager.UpdateEmployee(employee);
            return Ok(emp);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var exception = await manager.DeleteEmployee(id);
            if (exception == null)
                return NoContent();
            else 
            {
                return BadRequest(HelperMethods.getException(exception));
            }
        }
    }
}
