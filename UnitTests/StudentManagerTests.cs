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
                .Verify(r => r.GetAllStudents(), Times.Once);

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
                .Verify(r => r.GetStudent(1), Times.Once);
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
                .Verify(r => r.GetStudent(99999), Times.Once);
        }
        [Fact]
        public async void AddStudent()
        {
            var allCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };

            StudentVM studentModel = new StudentVM
            {
                Id = 0,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = new List<int> { 1 },
            };
            StudentResource studentResource = new StudentResource
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = allCourses.ListToResource(),
                teacher = new TeacherReource { Id = 1, Name = "Name", Degree = "Degree" }
            };
            Student student = studentModel.ToEntity();
            student.Id = 1;
            student.Teacher = new Teacher { Id = 1, Name = "Name", Degree = "Degree" };
            student.favCourses = new List<Course>{
                new Course { Id = 1, Name = "Name", Code = "Code" }
            };
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);
            repMock.Setup(r => r.AddStudent(It.IsAny<Student>())).ReturnsAsync(1);
            repMock.Setup(r => r.GetStudent(1)).ReturnsAsync(student);

            Manager manager = new Manager(repMock.Object);

            Assert.Equal(studentResource, await manager.AddStudent(studentModel));

            repMock
                .Verify(r => r.GetAllCourses(), Times.Once);
            repMock
                .Verify(r => r.AddStudent(It.IsAny<Student>()), Times.Once);
            repMock
                .Verify(r => r.GetStudent((int)student.Id), Times.Once);
        }
        [Fact]
        public async void DeleteStudent()
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
            repMock.Setup(r => r.GetStudent((int)student.Id)).ReturnsAsync(student);
            Exception exception = null;
            repMock.Setup(r => r.DeleteStudent((int)student.Id)).ReturnsAsync(exception);

            Manager manager = new Manager(repMock.Object);
            
            Assert.Null(await manager.DeleteStudent((int)student.Id));
            
            repMock
               .Verify(r => r.GetStudent((int)student.Id), Times.Once);
            repMock
               .Verify(r => r.DeleteStudent((int)student.Id), Times.Once);
        }
        [Fact]
        public async void DeleteStudentWrongId()
        {
            Student student = null;
            repMock.Setup(r => r.GetStudent(99999)).ReturnsAsync(student);
            Manager manager = new Manager(repMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await manager.DeleteStudent(99999));
            
            repMock
              .Verify(r => r.GetStudent(99999), Times.Once);
        }
        [Fact]
        public async void UpdateStudent()
        {
            StudentVM studentModel = new StudentVM
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = new List<int> { 1 },
            };
            var allCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };

            Manager manager = new Manager(repMock.Object);

            var studentEntity = studentModel.ToEntity();
            studentEntity.favCourses = allCourses;
            studentEntity.Teacher = new Teacher { Id = 1, Name = "Name", Degree = "Degree" };
            repMock.Setup(r => r.GetStudent((int)studentModel.Id)).ReturnsAsync(studentEntity);
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);
            repMock.Setup(r => r.UpdateStudent(It.IsAny<Student>())).ReturnsAsync(studentEntity);

            
            Assert.Equal(studentEntity.ToResource(), await manager.UpdateStudent(studentModel));
            repMock
                .Verify(r => r.GetStudent((int)studentModel.Id), Times.Once);
            repMock
                .Verify(r => r.GetAllCourses(), Times.Once);
            repMock
                .Verify(r => r.UpdateStudent(It.IsAny<Student>()), Times.Once);
        }
        [Fact]
        public async void UpdateStudentWrongId()
        {
            StudentVM studentModel = new StudentVM
            {
                Id = 99999,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = new List<int> { 1 },
            };
            Student student = null;
            var allCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };

            repMock.Setup(r => r.GetStudent((int) studentModel.Id)).ReturnsAsync(student);
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);

            Manager manager = new Manager(repMock.Object);
            await Assert.ThrowsAsync<Exception>(async () =>  await manager.UpdateStudent(studentModel));

            repMock
                .Verify(r => r.GetStudent((int)studentModel.Id), Times.Once);

        }
        [Fact]
        public async void UpdateStudentWrongCoursesId()
        {
            StudentVM studentModel = new StudentVM
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = new List<int> { 0 },
            };
            var allCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };
            Student student = studentModel.ToEntity();
            student.favCourses = allCourses;
            student.Teacher = new Teacher { Id = 1, Name = "Name", Degree = "Degree" };

            repMock.Setup(r => r.GetStudent((int)studentModel.Id)).ReturnsAsync(student);
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);

            Manager manager = new Manager(repMock.Object);
            await Assert.ThrowsAsync<Exception>(async () => await manager.UpdateStudent(studentModel));

            repMock
                .Verify(r => r.GetStudent((int)studentModel.Id), Times.Once);
            repMock
                .Verify(r => r.GetAllCourses(), Times.Once);

        }
    }

}
