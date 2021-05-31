using AutoMapper;
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
        public async Task<long> AddEmployee(Employee employee)
        {
            try
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
                return employee.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<Exception> DeleteEmployee(int Id)
        {
            try
            {
                var empToDelete = await dbContext.Employees.Where(e=>e.Id==Id).FirstOrDefaultAsync();
                dbContext.Employees.Remove(empToDelete);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await dbContext.Employees.Where(e => e.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Exception> UpdateEmployee(Employee employee)
        {
            try
            {
                dbContext.Employees.Update(employee);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}
