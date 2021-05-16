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
        public bool AddStudent(StudentVM student)
        {
            try
            {
                dbContext.Students.Add(_mapper.Map<Student>(student));
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteStudent(int Id)
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

        public async Task<List<StudentResource>> GetAllStudents()
        {
            return _mapper.Map<List<StudentResource>>(await dbContext.Students.Include(e=>e.favCourse).ToListAsync());
        }

        public async Task<StudentResource> GetStudent(int Id)
        {
            return _mapper.Map<StudentResource>(await dbContext.Students.Include(e=>e.favCourse).Where(e => e.Id == Id).FirstOrDefaultAsync());

        }

        public bool UpdateStudent(StudentVM student)
        {
            try
            {
                dbContext.Students.Update(_mapper.Map<Student>(student));
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
