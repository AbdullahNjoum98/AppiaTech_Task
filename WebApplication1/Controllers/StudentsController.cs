using Data;
using Data.Repos;
using Domain.Entities;
using Domain.IRepos;
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
        public Task<List<Student>> GetAllStudents()
        {
            return repository.GetAllStudents();
        }
        [HttpGet("{Id}")]
        public Task<Student> GetStudent(int Id)
        {
            return repository.GetStudent(Id);
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {

            if (repository.AddStudent(student))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateStudent([FromBody] Student student)
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
