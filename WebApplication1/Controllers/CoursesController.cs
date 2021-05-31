using Domain.IRepos;
using Cotracts.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace TaskAPI.Controllers
{

    [Route("Courses")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly IManager manager;

        public CoursesController(IManager manager)
        {
            this.manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await manager.GetAllCourses();
            return Ok(courses);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourse(int Id)
        {
            var course = await manager.GetCourse(Id);
            return Ok(course);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] FavCourseVM course)
        {
            var addedCourse = await manager.AddCourse(course);
            return Ok(addedCourse);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] FavCourseVM course)
        {
            var updatedCourse = await manager.UpdateCourse(course);
            return Ok(updatedCourse);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            Exception exception = await manager.DeleteCourse(id);
            if ( exception == null)
                return NoContent();
            else
            {
                return BadRequest(HelperMethods.getException(exception));
            }
        }
    }
}
