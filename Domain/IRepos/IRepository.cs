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
        public bool AddEmployee(EmployeeVM employee);
        public bool UpdateEmployee(EmployeeVM employee);
        public bool DeleteEmployee(int id);
        #endregion
        #region Students
        public Task<List<StudentResource>> GetAllStudents();
        public Task<StudentResource> GetStudent(int Id);
        public bool AddStudent(StudentVM student);
        public bool UpdateStudent(StudentVM student);
        public bool DeleteStudent(int Id);
        #endregion

    }
}
