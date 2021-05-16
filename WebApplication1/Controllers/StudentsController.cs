using Data;
using Data.Repos;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Students")]
    public class StudentsController : Controller
    {
        private readonly IRepository repository;

        public StudentsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public Task<List<StudentResource>> GetAllStudents()
        {
            return repository.GetAllStudents();
        }
        [HttpGet("{Id}")]
        public Task<StudentResource> GetStudent(int Id)
        {
            return repository.GetStudent(Id);
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentVM student)
        {

            if (repository.AddStudent(student))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateStudent([FromBody] StudentVM student)
        {
            if (repository.UpdateStudent(student))
                return Ok();
            else
                return BadRequest();
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteStudent(int Id)
        {
            if (repository.DeleteStudent(Id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
