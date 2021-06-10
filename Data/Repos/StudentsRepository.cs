using Domain.Entities;
using Domain.IRepos;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public partial class Repository : IRepository
    {
        public async Task<long> AddStudent(Student student)
        {
            try
            {
                var teacher = await dbContext.Teachers.Where(e => student.TeacherId==e.Id).FirstOrDefaultAsync();
                if (teacher == null) 
                    throw new Exception("Teacher id is not found");
                student.Teacher = teacher;
                
                await dbContext.Students.AddAsync(student);
                await dbContext.SaveChangesAsync();
                var id = student.Id;
                //await elasticClient.IndexDocumentAsync(student);

                return student.Id;
            }  
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Exception> DeleteStudent(int Id)
        {
            try
            {
                var studentToDelete = await dbContext.Students.Include(e=>e.favCourses).Include(e => e.Teacher).Where(e => e.Id == Id).FirstOrDefaultAsync();
                dbContext.Students.Remove(studentToDelete);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await dbContext.Students.Include(e=>e.favCourses).Include(e=>e.Teacher).ToListAsync();
        }

        public async Task<Student> GetStudent(int Id)
        {
            var student = await dbContext.Students.Include(e=>e.favCourses).Include(e => e.Teacher).Where(e => e.Id == Id).FirstOrDefaultAsync();
            return student;
        }

        public async Task<Exception> UpdateStudent(Student student)
        {
            try
            {
                var teacher = await dbContext.Teachers.Where(e => student.TeacherId == e.Id).FirstOrDefaultAsync();
                if (teacher == null)
                    throw new Exception("Teacher id is not found");
                student.Teacher = teacher;

                dbContext.Students.Update(student);
                await dbContext.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
