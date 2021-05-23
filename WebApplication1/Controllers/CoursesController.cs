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
    [ApiController]
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
            var Id = repository.AddCourse(course);
            if (Id != 0)
                return repository.GetCourse((int)course.Id);
            else
                return null;
        }
        [HttpPut]
        public Task<FavCourseResource> UpdateCourse([FromBody] FavCourseVM course)
        {
            Exception excepton = repository.UpdateCourse(course);
            if (excepton == null)
                return repository.GetCourse((int)course.Id);
            else
                return null;
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteCourse(int id)
        {
            Exception exception = repository.DeleteCourse(id);
            if ( exception == null)
                return Ok();
            else
            {
                return BadRequest(HelperMethods.getException(exception));
            }
        }
    }
}
