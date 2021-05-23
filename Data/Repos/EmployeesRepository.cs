﻿using AutoMapper;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
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
        private readonly IMapper _mapper;

        public Repository(ProjectDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }
        public long AddEmployee(EmployeeVM employee)
        {
            try
            {
                var entity = _mapper.Map<Employee>(employee);
                dbContext.Employees.Add(entity);
                dbContext.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public Exception DeleteEmployee(int Id)
        {
            try
            {
                var empToDelete = dbContext.Employees.Where(e=>e.Id==Id).FirstOrDefault();
                dbContext.Employees.Remove(empToDelete);
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }

        public async Task<List<EmployeeResource>> GetAllEmployees()
        {
            return _mapper.Map<List<EmployeeResource>>(await dbContext.Employees.ToListAsync());
        }

        public async Task<EmployeeResource> GetEmployee(int Id)
        {
            return _mapper.Map<EmployeeResource>(await dbContext.Employees.Where(e => e.Id == Id).FirstOrDefaultAsync());
        }

        public Exception UpdateEmployee(EmployeeVM employee)
        {
            try
            {
                dbContext.Employees.Update(_mapper.Map<Employee>(employee));
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}
