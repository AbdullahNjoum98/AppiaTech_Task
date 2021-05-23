using Data;
using Data.Repos;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Students")]
    [ApiController]
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
        public async Task<StudentResource> AddStudent([FromBody] StudentVM student)
        {
            var id = repository.AddStudent(student);
            if(id != 0)
            {
                var newStudent= await repository.GetStudent((int)id);
                return newStudent;
            }
            else
                return null;

        }
        [HttpPut]
        public Task<StudentResource> UpdateStudent([FromBody] StudentVM student)
        {
             var exception = repository.UpdateStudent(student);
            if (exception == null)
                return repository.GetStudent((int)student.Id);
            else
                return null;
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteStudent(int Id)
        {
            var exception = repository.DeleteStudent(Id);
            if (exception == null)
                return Ok();
            else 
                return BadRequest(HelperMethods.getException(exception));
        }
      }
    } 

