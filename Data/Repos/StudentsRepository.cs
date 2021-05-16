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
        public bool AddStudent(Student student)
        {
            try
            {
                dbContext.Students.Add(student);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteStudent(long Id)
        {
            try
            {
                var studentToDelete = dbContext.Students.Include(e=>e.favCourse).Where(e => e.Id == Id).FirstOrDefault();
                dbContext.Students.Remove(studentToDelete);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Student>> GetAllStudents()
        {
            return dbContext.Students.Include(e=>e.favCourse).ToListAsync();
        }

        public Task<Student> GetStudent(long Id)
        {
            return dbContext.Students.Include(e=>e.favCourse).Where(e => e.Id == Id).FirstOrDefaultAsync();

        }

        public bool UpdateStudent(Student student)
        {
            try
            {
                dbContext.Students.Update(student);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
