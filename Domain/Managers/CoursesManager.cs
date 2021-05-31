using Cotracts.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Mappers;

namespace Domain.Managers
{
    public partial class Manager : IManager 
    {
        public async Task<FavCourseResource> AddCourse(FavCourseVM course)
        {
            var Id = await repository.AddCourse(course.ToEntity());
            if (Id != 0)
            {
                var entity = await repository.GetCourse((int)Id);
                return entity.ToResource();
            }
            else
                return null;
        }

        public async Task<List<FavCourseResource>> GetAllCourses()
        {
            var entities = await repository.GetAllCourses();
            return entities.ListToResource();
        }
        public async Task<FavCourseResource> GetCourse(int Id)
        {
            var entity = await repository.GetCourse(Id);
            if (entity==null)
                throw new Exception("Id is not found");
            return entity.ToResource();
        }

        public async Task<FavCourseResource> UpdateCourse(FavCourseVM course)
        {
            var courseCheck = await repository.GetCourse((int)course.Id);
            if (courseCheck == null)
                throw new Exception("Id is not found");
            courseCheck.Name = course.Name;
            courseCheck.Code = course.Code;
            Exception excepton = await repository.UpdateCourse(courseCheck);
            if (excepton == null)
            {
                var entity = await repository.GetCourse((int)course.Id);
                return entity.ToResource();
            }
            else
                return null;
        }
        public async Task<Exception> DeleteCourse(int Id)
        {
            var course = await repository.GetCourse(Id);
            if (course == null)
                throw new Exception("Id is not found");

            return await repository.DeleteCourse(Id); ;
        }
    }
}
