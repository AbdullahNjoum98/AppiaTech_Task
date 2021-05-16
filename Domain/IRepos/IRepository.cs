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
        public Task<Employee> GetEmployee(long Id);
        public bool AddEmployee(Employee employee);
        public bool UpdateEmployee(Employee employee);
        public bool DeleteEmployee(long id);
        #endregion
        #region Students
        public Task<List<Student>> GetAllStudents();
        public Task<Student> GetStudent(long Id);
        public bool AddStudent(Student student);
        public bool UpdateStudent(Student student);
        public bool DeleteStudent(long Id);
        #endregion

    }
}
