using Cotracts.VMs;
using Domain;
using Domain.Entities;
using Domain.IRepos;
using Domain.Managers;
using Domain.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class StudentManagerTests
    {
        public Mock<IRepository> repMock = new Mock<IRepository>();
        [Fact]
        public async void GetAllStudents()
        {
            List<Student> students = new List<Student> {
                new Student
                {
                    Id=1,
                    Name="Name1",
                    Email="Email@any.com",
                    Phone ="0597872141",
                    Teacher = new Teacher { Id=1, Name ="Name", Degree="Degree"},
                    favCourses= new List<Course> { new Course { Id=1,Code ="Code", Name ="Name"} },
                    TeacherId=1
                }
            };
            var resources = students.ListToResource();
            repMock.Setup(e => e.GetAllStudents()).ReturnsAsync(students);
            Manager manager = new Manager(repMock.Object);
            var result = await manager.GetAllStudents();

            Assert.Equal(resources.Count, result.Count);
            repMock
                .Verify(r => r.GetAllStudents(), Times.Exactly(1));

        }
        [Fact]
        public async void GetStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                Teacher = new Teacher { Id = 1, Name = "Name", Degree = "Degree" },
                favCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } },
                TeacherId = 1
            };
            StudentResource resource = student.ToResource();
            repMock.Setup(e => e.GetStudent(1)).ReturnsAsync(student);
            Manager manager = new Manager(repMock.Object);
            var result = await manager.GetStudent(1);

            Assert.Equal(resource, result);
            repMock
                .Verify(r => r.GetStudent(1), Times.Exactly(1));
        }
        [Fact]
        public async void GetStudentWrongId()
        {
            Student student = null;
            repMock.Setup(e => e.GetStudent(99999)).ReturnsAsync(student);
            Manager manager = new Manager(repMock.Object);
            //var result = await manager.GetStudent();

            await Assert.ThrowsAsync<Exception>(async () => await manager.GetStudent(99999));
            repMock
                .Verify(r => r.GetStudent(99999), Times.Exactly(1));
        }
        [Fact]
        public async void AddStudent()
        {
            var allCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };
            
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);

            StudentVM studentModel = new StudentVM
            {
                Id = 0,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = new List<int> { 1 },
                teacher = 1
            };
            StudentResource studentResource = new StudentResource
            {
                Id = 0,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = allCourses.ListToResource(),
                teacher = new TeacherReource { Id=1, Name="Name", Degree="Degree"}
            };
            Student student = studentModel.ToEntity();
            student.Id = 1;

            repMock.Setup(r => r.AddStudent(student)).ReturnsAsync(1);

            repMock.Setup(r => r.GetStudent(1)).ReturnsAsync(student);

           
            Manager manager = new Manager(repMock.Object);
            var value = await manager.AddStudent(studentModel);
            student.favCourses = new List<Course> 
            {
                new Course 
                { 
                    Id = 1,
                    Code = "Code",
                    Name = "Name"
                } 
            };
            

            Assert.Equal(value, studentResource);

            repMock
                .Verify(r => r.GetAllCourses(), Times.Exactly(1));
            repMock
                .Verify(r => r.AddStudent(student), Times.Exactly(1));
            repMock
                .Verify(r => r.GetStudent((int)student.Id), Times.Exactly(1));
        }
    }
}
