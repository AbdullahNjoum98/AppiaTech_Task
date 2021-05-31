using Domain.Entities;
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
        public Task<List<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployee(int Id);
        public Task<long> AddEmployee(Employee employee);
        public Task<Exception> UpdateEmployee(Employee employee);
        public Task<Exception> DeleteEmployee(int id);
        #endregion
        #region Students
        public Task<List<Student>> GetAllStudents();
        public Task<Student> GetStudent(int Id);
        public Task<long> AddStudent(Student student);
        public Task<Exception> UpdateStudent(Student student);
        public Task<Exception> DeleteStudent(int Id);
        #endregion
        #region
        public Task<List<Course>> GetAllCourses();
        public Task<Course> GetCourse(int Id);
        public Task<int> AddCourse(Course student);
        public Task<Exception> UpdateCourse(Course student);
        public Task<Exception> DeleteCourse(int Id);

        #endregion

    }
}
