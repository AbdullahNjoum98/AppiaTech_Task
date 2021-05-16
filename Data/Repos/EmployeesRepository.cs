using Domain.Entities;
using Domain.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public partial class Repository : IRepository
    {
        private readonly ProjectDbContext dbContext;

        public Repository(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AddEmployee(Employee employee)
        {
            try
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteEmployee(long Id)
        {
            try
            {
                var empToDelete = dbContext.Employees.Where(e=>e.Id==Id).FirstOrDefault();
                dbContext.Employees.Remove(empToDelete);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public Task<List<Employee>> GetAllEmployees()
        {
            return dbContext.Employees.ToListAsync();
        }

        public Task<Employee> GetEmployee(long Id)
        {
            return dbContext.Employees.Where(e => e.Id == Id).FirstOrDefaultAsync();
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                dbContext.Employees.Update(employee);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
