using AutoMapper;
using Data;
using Domain.Entities;
using Domain.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class TeacherServices
    {
        private readonly ProjectDbContext dBContext;
        private readonly IMapper mapper;
        private readonly ServiceProvider provider;


        public TeacherServices(ServiceProvider serviceProvider)//ProjectDbContext dBContext, IMapper mapper)
        {
            //this.dBContext = dBContext;
            //this.mapper = mapper;
        }
        public Exception AddTeacher(TeacherVM teacher)
        {
            var dbContext=provider.GetService<ProjectDbContext>();
            try
            {
                dbContext.Teachers.Add(mapper.Map<Teacher>(teacher));
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

        public Exception DeleteTeacher(int id)
        {
            try
            {
                var teacherToDelete = dBContext.Teachers.Where(e => e.Id == id).FirstOrDefault();
                dBContext.Teachers.Remove(teacherToDelete);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<TeacherReource>> GetAllTeachers()
        {
            return mapper.Map<List<TeacherReource>>(await dBContext.Teachers.ToListAsync());
        }

        public async Task<TeacherReource> GetTeacher(int Id)
        {
            return mapper.Map<TeacherReource>(await dBContext.Teachers.Where(e => e.Id == Id).FirstOrDefaultAsync());
        }

        public Exception UpdateTeacher(TeacherVM teacher)
        {
            try
            {
                dBContext.Teachers.Update(mapper.Map<Teacher>(teacher));
                dBContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
