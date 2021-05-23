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
            this.CreateMap<Student, StudentResource>().ReverseMap().MaxDepth(3);
            this.CreateMap<Student, StudentVM>().ReverseMap().MaxDepth(3);
            this.CreateMap<Person, PersonResource>().ReverseMap().MaxDepth(3);
            this.CreateMap<Person, PersonVM>().ReverseMap().MaxDepth(3);
            this.CreateMap<Employee, EmployeeResource>().ReverseMap().MaxDepth(3);
            this.CreateMap<Employee, EmployeeVM>().ReverseMap().MaxDepth(3);
            this.CreateMap<Course, FavCourseVM>().ReverseMap().MaxDepth(3);
            this.CreateMap<Course, FavCourseResource>().ReverseMap().MaxDepth(3);
            this.CreateMap<Teacher, TeacherReource>().ReverseMap().MaxDepth(3);
            this.CreateMap<Teacher, TeacherVM>().ReverseMap().MaxDepth(3);
        }
    }
}
