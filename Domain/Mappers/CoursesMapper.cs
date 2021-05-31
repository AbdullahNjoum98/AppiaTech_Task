using Cotracts.VMs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public static class CoursesMapper
    {
        public static FavCourseResource ToResource(this Course course)
        {
            return new FavCourseResource()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
            };
        }
        public static List<FavCourseResource> ListToResource(this List<Course> courses)
        {
            var returnValue = new List<FavCourseResource>();
            foreach (var course in courses)
            {
                returnValue.Add(
                    new FavCourseResource()
                    {
                        Id = course.Id,
                        Code = course.Code,
                        Name = course.Name,
                    }
                    );
            }
            return returnValue;
        }
        public static Course ToEntity(this FavCourseVM course)
        {
            return new Course()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name
            };
        }
        public static List<Course> ListToEntity(this List<FavCourseVM> courses)
        {
            var returnValue = new List<Course>();
            foreach (var course in courses)
            {
                returnValue.Add(
                    new Course()
                    {
                        Id = course.Id,
                        Code = course.Code,
                        Name = course.Name,
                    }
                    );
            }
            return returnValue;
        }
    }
}
