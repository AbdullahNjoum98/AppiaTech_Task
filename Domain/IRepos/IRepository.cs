using Domain.Entities;
using Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepos
{
    public interface IRepository
    {
        #region Employees
        public Task<List<EmployeeResource>> GetAllEmployees();
        public Task<EmployeeResource> GetEmployee(int Id);
        public Exception AddEmployee(EmployeeVM employee);
        public Exception UpdateEmployee(EmployeeVM employee);
        public Exception DeleteEmployee(int id);
        #endregion
        #region Students
        public Task<List<StudentResource>> GetAllStudents();
        public Task<StudentResource> GetStudent(int Id);
        public Exception AddStudent(StudentVM student);
        public Exception UpdateStudent(StudentVM student);
        public Exception DeleteStudent(int Id);
        #endregion
        #region
        public Task<List<FavCourseResource>> GetAllCourses();
        public Task<FavCourseResource> GetCourse(int Id);
        public Exception AddCourse(FavCourseVM student);
        public Exception UpdateCourse(FavCourseVM student);
        public Exception DeleteCourse(int Id);

        #endregion

    }
}
