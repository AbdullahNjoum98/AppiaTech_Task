using Domain.IRepos;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{

    [Route("Courses")]

    public class CoursesController : Controller
    {
        private readonly IRepository repository;

        public CoursesController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public Task<List<FavCourseResource>> GetAllCourses()
        {
            return repository.GetAllCourses();
        }
        [HttpGet("{Id}")]
        public Task<FavCourseResource> GetCourse(int Id)
        {
            return repository.GetCourse(Id);
        }
        [HttpPost]
        public Task<FavCourseResource> AddCourse([FromBody] FavCourseVM course)
        {
            repository.AddCourse(course);
            return repository.GetCourse((int)course.Id);
        }
        [HttpPut]
        public Task<FavCourseResource> UpdateCourse([FromBody] FavCourseVM course)
        {
            repository.UpdateCourse(course);
            return repository.GetCourse((int)course.Id);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteCourse(int id)
        {
            if (repository.DeleteCourse(id) == null)
                return Ok();
            else
            {
                string exception=HelperMethods.getException(repository.DeleteCourse(id));
                return BadRequest(exception);
            }
        }
    }
}
