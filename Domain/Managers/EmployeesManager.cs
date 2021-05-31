using Cotracts.VMs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Mappers;

namespace Domain.Managers
{
    public partial class Manager : IManager
    {

        public async Task<EmployeeResource> AddEmployee(EmployeeVM employee)
        {
            var entity = employee.ToEntity();
            var Id =  await repository .AddEmployee(entity);
            if (Id != 0)
            {
                var employeeResource = await repository.GetEmployee((int)Id);
                return employeeResource.ToResource();
            }
            else
                return null;
        }
        public async Task<Exception> DeleteEmployee(int id)
        {
            var entity = await repository.GetEmployee(id);
            if (entity == null)
                throw new Exception("Id is not found");
            return await repository.DeleteEmployee(id);
        }
        public async Task<List<EmployeeResource>> GetAllEmployees()
        {
            var entities = await repository.GetAllEmployees();
            return entities.ListToResource();
        }
        public async Task<EmployeeResource> GetEmployee(int Id)
        {
            var entity = await repository.GetEmployee(Id);
            if (entity == null)
                throw new Exception("Id is not found");
            return entity.ToResource();
        }
        public async Task<EmployeeResource> UpdateEmployee(EmployeeVM employee)
        {
            var emp = await repository.GetEmployee((int)employee.Id);
            if (emp == null)
                throw new Exception("Id is not found");
            emp.Name = employee.Name;
            emp.Phone = employee.Phone;
            emp.Salary = employee.Salary;
            var exception = repository.UpdateEmployee(emp);
            if (exception == null)
            {
                var entity = await repository.GetEmployee((int)employee.Id);
                return entity.ToResource();
            }
            else
                return null;
        }
    }
}
