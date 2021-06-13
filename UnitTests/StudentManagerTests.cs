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
        private static int wrongId = int.MaxValue;
        private static Teacher teacher = new Teacher { Id = 1, Name = "Name", Degree = "Degree" };
        private static List<int> favCourseIds = new List<int> { 1 };
        private static List<int> wrongFavCourseIds = new List<int> { wrongId };
        private static List<Course> studentfavCourses = new List<Course> { new Course { Id = 1, Code = "Code", Name = "Name" } };
        private static List<Course> allCourses = new List<Course> {
            new Course { Id = 1, Code = "Code", Name = "Name" },
            new Course { Id = 2, Code = "Code2", Name = "Name2" }
        };
        private static List<Student> students = new()
        {
            new Student
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                Teacher = teacher,
                favCourses = studentfavCourses,
                TeacherId = 1
            }
        };
        private static Student student = new Student
        {
            Id = 1,
            Name = "Name1",
            Email = "Email@any.com",
            Phone = "0597872141",
            Teacher = teacher,
            favCourses = studentfavCourses,
            TeacherId = 1
        };

        private static StudentVM studentModel = new()
        {
            Id = 0,
            Name = "Name1",
            Email = "Email@any.com",
            Phone = "0597872141",
            favCourses = favCourseIds,
        };
        private static StudentResource studentResource = new()
        {
            Id = 1,
            Name = "Name1",
            Email = "Email@any.com",
            Phone = "0597872141",
            favCourses = studentfavCourses.ListToResource(),
            teacher = teacher.ToResource()
        };

        public Mock<IRepository> repMock = new Mock<IRepository>();
        [Fact]
        public async void GetAllStudents()
        {
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
            repMock.Setup(e => e.GetStudent(wrongId)).ReturnsAsync(student);
            Manager manager = new Manager(repMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await manager.GetStudent(wrongId));
            repMock
                .Verify(r => r.GetStudent(wrongId), Times.Once);
        }
        [Fact]
        public async void AddStudent()
        {
            Student student = studentModel.ToEntity();
            student.Id = 1;
            student.Teacher = teacher;
            student.favCourses = studentfavCourses;
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);
            repMock.Setup(r => r.AddStudent(It.IsAny<Student>())).ReturnsAsync(studentResource.Id);
            repMock.Setup(r => r.GetStudent((int)studentResource.Id)).ReturnsAsync(student);

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
            repMock.Setup(r => r.GetStudent(wrongId)).ReturnsAsync(student);
            Manager manager = new Manager(repMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await manager.DeleteStudent(wrongId));

            repMock
              .Verify(r => r.GetStudent(wrongId), Times.Once);
        }
        [Fact]
        public async void UpdateStudent()
        {
            Manager manager = new Manager(repMock.Object);
            var studentEntity = studentModel.ToEntity();
            studentEntity.favCourses = studentfavCourses;
            studentEntity.Teacher = teacher;
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
            studentModel.Id = wrongId;
            Student student = null;

            repMock.Setup(r => r.GetStudent((int)studentModel.Id)).ReturnsAsync(student);
            repMock.Setup(r => r.GetAllCourses()).ReturnsAsync(allCourses);

            Manager manager = new Manager(repMock.Object);
            await Assert.ThrowsAsync<Exception>(async () => await manager.UpdateStudent(studentModel));

            repMock
                .Verify(r => r.GetStudent((int)studentModel.Id), Times.Once);

        }
        [Fact]
        public async void UpdateStudentWrongCoursesId()
        {
            StudentVM studentModel = new()
            {
                Id = 1,
                Name = "Name1",
                Email = "Email@any.com",
                Phone = "0597872141",
                favCourses = wrongFavCourseIds,
            };
            Student student = studentModel.ToEntity();
            student.favCourses = studentfavCourses;
            student.Teacher = teacher;

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
