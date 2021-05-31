using Cotracts.VMs;
using Domain.IRepos;
using Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public partial class Manager : IManager
    {
        private readonly IRepository repository;
        public Manager(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<StudentResource> AddStudent(StudentVM student)
        {

            var allCourses = await repository.GetAllCourses();
            var studentCourses = allCourses.Where(e => student.favCourses.Contains(e.Id)).ToList();
            if (studentCourses.Count != student.favCourses.Count)
                throw new Exception("One or more course id is not found");
            var studentEntity = student.ToEntity();
            studentEntity.favCourses = studentCourses;
            var id = await repository.AddStudent(studentEntity);
            if (id != 0)
            {
                var newStudent = await repository.GetStudent((int)id);
                return newStudent.ToResource();
            }
            else
                return null;
        }
        public async Task<Exception> DeleteStudent(int Id)
        {
            var student = await repository.GetStudent(Id);
            if (student == null)
                throw new Exception("Id is not found");
            return await repository.DeleteStudent(Id);
        }
        public async Task<List<StudentResource>> GetAllStudents()
        {
            var studentResources = await repository.GetAllStudents();
            return studentResources.ListToResource();
        }

        public async Task<StudentResource> GetStudent(int Id)
        {
            var student = await repository.GetStudent(Id);
            if (student == null)
                throw new Exception("Id is not found");
            return student.ToResource();
        }
        public async Task<StudentResource> UpdateStudent(StudentVM student)
        {
            var studentToUpdate = await repository.GetStudent((int)student.Id);
            if (studentToUpdate == null)
                throw new Exception("Id is not found");
            var allCourses = await repository.GetAllCourses();
            var studentCourses = allCourses.Where(e => student.favCourses.Contains(e.Id)).ToList();
            if(studentCourses.Count != student.favCourses.Count)
                throw new Exception("Some Course Ids are Not Found");
            
            studentToUpdate.Name = student.Name;
            studentToUpdate.Email = student.Email;
            studentToUpdate.Phone = student.Phone;
            studentToUpdate.TeacherId = student.teacher;
            studentToUpdate.favCourses = studentCourses;

            var exception = repository.UpdateStudent(studentToUpdate);
            if (exception == null)
            {
                var returnedEntity = await repository.GetStudent((int)student.Id);
                return returnedEntity.ToResource();
            }
            else
                return null;
        }
    }
}
