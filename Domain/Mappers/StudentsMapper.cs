using Cotracts.VMs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public static class StudentsMapper
    {
        public static StudentResource ToResource(this Student student)
        {
            return new StudentResource()
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
                favCourses = student.favCourses.ListToResource(),
                Phone = student.Phone,
                teacher = student.Teacher.ToResource()
            };
        }
        public static List<StudentResource> ListToResource(this List<Student> students)
        {
            var returnValue = new List<StudentResource>();
            foreach (var student in students)
            {
                returnValue.Add(new StudentResource()
                {
                    Id = student.Id,
                    Email = student.Email,
                    Name = student.Name,
                    favCourses = student.favCourses.ListToResource(),
                    Phone = student.Phone,
                    teacher = TeachersMapper.ToResource(student.Teacher)
                });
            }
            return returnValue;
        }
        public static Student ToEntity(this StudentVM student)
        {
            return new Student()
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
                Phone = student.Phone,
                TeacherId = student.teacher
            };
        }
        public static List<Student> ListToEntity(this List<StudentVM> students)
        {
            var returnValue = new List<Student>();
            foreach (var student in students)
            {
                returnValue.Add(new Student()
                {
                    Id = student.Id,
                    Email = student.Email,
                    Name = student.Name,
                    Phone = student.Phone,
                    TeacherId = student.teacher
                });
            }
            return returnValue;
        }
    }
}

