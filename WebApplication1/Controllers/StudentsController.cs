using Cotracts.VMs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Students")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IManager manager;

        public StudentsController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await manager.GetAllStudents();
            return Ok(students);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudent(int Id)
        {
            var student = await manager.GetStudent(Id);
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentVM student)
        {
            var studentAdded = await manager.AddStudent(student);
            return Ok(studentAdded);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentVM student)
        {
            var studentUpdated = await manager.UpdateStudent(student);
            if (studentUpdated != null)
                return Ok(studentUpdated);
            else
                return BadRequest();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var exception = await manager.DeleteStudent(Id);
            if (exception == null)
                return NoContent();
            else 
                return BadRequest(HelperMethods.getException(exception));
        }
      }
    } 

