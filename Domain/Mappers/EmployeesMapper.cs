using Cotracts.VMs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    public static class EmployeesMapper
    {
        public static EmployeeResource ToResource(this Employee employee)
        {
            return new EmployeeResource()
            {
                Id = employee.Id,
                Name=employee.Name,
                Email=employee.Email,
                Phone=employee.Phone,
                Salary=employee.Salary
            };
        }
        public static List<EmployeeResource> ListToResource(this List<Employee> employees)
        {
            var returnValue = new List<EmployeeResource>();
            foreach (var employee in employees)
            {
                returnValue.Add(
                    new EmployeeResource()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Salary = employee.Salary,
                        Phone= employee.Phone
                    }
                    );
            }
            return returnValue;
        }
        public static Employee ToEntity(this EmployeeVM employee)
        {
            return new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
        }
        public static List<Employee> ListToEntity(this List<EmployeeVM> employees)
        {
            var returnValue = new List<Employee>();
            foreach (var employee in employees)
            {
                returnValue.Add(
                    new Employee()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Phone = employee.Phone,
                        Salary = employee.Salary
                    }
                    );
            }
            return returnValue;
        }
    }
}
