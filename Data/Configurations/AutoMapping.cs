using AutoMapper;
using Domain.Entities;
using Domain.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Configurations
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<Student, StudentResource>().ReverseMap();
            this.CreateMap<Student, StudentVM>().ReverseMap();
            this.CreateMap<Person, PersonResource>().ReverseMap();
            this.CreateMap<Person, PersonVM>().ReverseMap();
            this.CreateMap<Employee, EmployeeResource>().ReverseMap();
            this.CreateMap<Employee, EmployeeVM>().ReverseMap();
            this.CreateMap<Course, CourseVM>().ReverseMap();
        }
    }
}
