using Cotracts.VMs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public static class TeachersMapper
    {
        public static TeacherReource ToResource(this Teacher teacher)
        {
            return new TeacherReource()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Degree = teacher.Degree
            };
        }
        public static List<TeacherReource> ListToResource(this List<Teacher> teachers)
        {
            var returnValue = new List<TeacherReource>();
            foreach (var teacher in teachers)
            {
                returnValue.Add(new TeacherReource()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Degree = teacher.Degree
                });
            }
            return returnValue;
        }
        public static Teacher ToEntity(this TeacherVM teacher)
        {
            return new Teacher()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Degree = teacher.Degree
            };
        }
        public static List<Teacher> ListToEntity(this List<TeacherVM> teachers)
        {
            var returnValue = new List<Teacher>();
            foreach (var teacher in teachers)
            {
                returnValue.Add(new Teacher()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Degree = teacher.Degree,
                    
                });
            }
            return returnValue;
        }
    }
}
