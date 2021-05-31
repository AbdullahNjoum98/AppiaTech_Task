using Cotracts.VMs;
using Domain.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IManager
    {
        #region Empolyees 
        public Task<List<EmployeeResource>> GetAllEmployees();
        public Task<EmployeeResource> GetEmployee(int Id);
        public Task<EmployeeResource> AddEmployee(EmployeeVM employee);
        public Task<EmployeeResource> UpdateEmployee(EmployeeVM employee);
        public Task<Exception> DeleteEmployee(int id);
        #endregion
        #region Students
        public Task<List<StudentResource>> GetAllStudents();
        public Task<StudentResource> GetStudent(int Id);
        public Task<StudentResource> AddStudent(StudentVM student);
        public Task<StudentResource> UpdateStudent(StudentVM student);
        public Task<Exception> DeleteStudent(int Id);
        #endregion
        #region Courses
        public Task<List<FavCourseResource>> GetAllCourses();
        public Task<FavCourseResource> GetCourse(int Id);
        public Task<FavCourseResource> AddCourse(FavCourseVM course);
        public Task<FavCourseResource> UpdateCourse(FavCourseVM course);
        public Task<Exception> DeleteCourse(int Id);

        #endregion
    }
}
