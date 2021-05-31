using AutoMapper;
using Domain.Entities;
using Domain.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data.Repos
{
    public partial class Repository : IRepository
    {
        public async Task<int> AddCourse(Course course)
        {
            try
            {
                await dbContext.Courses.AddAsync(course);
                await dbContext.SaveChangesAsync();
                return course.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<Exception> DeleteCourse(int Id)
        {
            try
            {
                var courseToDelete = await dbContext.Courses.Where(e => e.Id == Id).FirstOrDefaultAsync();
                dbContext.Courses.Remove(courseToDelete);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourse(int Id)
        {
            return await dbContext.Courses.Where(e => e.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Exception> UpdateCourse(Course course)
        {
            try
            {
                dbContext.Courses.Update(course);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}
