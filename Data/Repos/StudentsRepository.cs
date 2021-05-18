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
                var studentMapped = _mapper.Map<Student>(student);
                var any = dbContext.Courses.Where(e => student.favCourses.Select(e => e.Id).Contains(e.Id));
                studentMapped.favCourses = any.ToList();
                dbContext.Students.Add(studentMapped);
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
                dbContext.Students.Update(_mapper.Map<Student>(student));
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
