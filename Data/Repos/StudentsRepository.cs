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
        public Exception AddStudent(StudentVM student)
        {
            try
            {
                var courses = dbContext.Courses.Where(e => student.favCourses.Contains(e.Id)).ToList();
                Student studentToAdd = new Student
                {
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.Phone,
                    favCourses = courses
                };
                dbContext.Students.Add(studentToAdd);
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }


        public Exception DeleteStudent(int Id)
        {
            try
            {
                var studentToDelete = dbContext.Students.Include(e=>e.favCourses).Where(e => e.Id == Id).FirstOrDefault();
                dbContext.Students.Remove(studentToDelete);
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<StudentResource>> GetAllStudents()
        {
            return _mapper.Map<List<StudentResource>>(await dbContext.Students.Include(e=>e.favCourses).ToListAsync());
        }

        public async Task<StudentResource> GetStudent(int Id)
        {
            return _mapper.Map<StudentResource>(await dbContext.Students.Include(e=>e.favCourses).Where(e => e.Id == Id).FirstOrDefaultAsync());

        }

        public Exception UpdateStudent(StudentVM student)
        {
            try
            {
                var courses = dbContext.Courses.Where(e => student.favCourses.Contains(e.Id)).ToList();
                Student studentToAdd = new Student
                {
                    Id=student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.Phone,
                    favCourses = courses
                };
                dbContext.Students.Update(studentToAdd);
                dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
