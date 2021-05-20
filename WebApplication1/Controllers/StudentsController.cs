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
        public Task<StudentResource> AddStudent([FromBody] StudentVM student)
        {
            var jsonString=JsonSerializer.Serialize(student);
            var bytesObject=Encoding.UTF8.GetBytes(jsonString);
            HelperMethods.Producer(bytesObject);
            repository.AddStudent(student);
            return repository.GetStudent((int)student.Id);
        }
        [HttpPut]
        public Task<StudentResource> UpdateStudent([FromBody] StudentVM student)
        {
             repository.UpdateStudent(student);
             return repository.GetStudent((int)student.Id);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteStudent(int Id)
        {
            if (repository.DeleteStudent(Id) == null)
                return Ok();
            else {
                string exception = HelperMethods.getException(repository.DeleteStudent(Id));
                return BadRequest(exception);
            }
            }
        }
    } 

