using AutoMapper;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
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
        public Exception AddCourse(FavCourseVM course)
        {
            try
            {
                dbContext.Courses.Add(_mapper.Map<Course>(course));
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }


        public Exception DeleteCourse(int Id)
        {
            try
            {
                var courseToDelete = dbContext.Courses.Where(e => e.Id == Id).FirstOrDefault();
                dbContext.Courses.Remove(courseToDelete);
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<FavCourseResource>> GetAllCourses()
        {
            return _mapper.Map<List<FavCourseResource>>(await dbContext.Courses.ToListAsync());
        }

        public async Task<FavCourseResource> GetCourse(int Id)
        {
            return _mapper.Map<FavCourseResource>(await dbContext.Courses.Where(e => e.Id == Id).FirstOrDefaultAsync());
        }

        public Exception UpdateCourse(FavCourseVM course)
        {
            try
            {
                dbContext.Courses.Update(_mapper.Map<Course>(course));
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}
