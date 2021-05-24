using AutoMapper;
using Data;
using Domain.Entities;
using Domain.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskAPI;

namespace Consumer
{
    public class TeacherServices
    {
        private readonly ProjectDbContext dBContext;
        private readonly IMapper mapper;
        private const string host = "https://localhost:44398/Teachers";

        public TeacherServices(ProjectDbContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }
        public async Task Harvest()
        {
            var originalTeachers = mapper.Map<List<Teacher>>(await this.GetAllTeachers());
            List<Teacher> currentTeachers = new List<Teacher>();

            currentTeachers = await dBContext.Teachers.ToListAsync();
            if (currentTeachers.Count == 0 && originalTeachers.Count == 0)
            {
                return;
            }
            List<Teacher> teachersToAdd = new List<Teacher>();

            List<Teacher> teachersToUpdate = new List<Teacher>();

            List<Teacher> teachersToDelete = new List<Teacher>();

            if(originalTeachers.Count == 0)
            {
                teachersToDelete = currentTeachers;
            }

            foreach (var originalTeacher in originalTeachers)
            {
                if (currentTeachers.Count == 0)
                {
                    teachersToAdd.Add(originalTeacher);
                }
                foreach (var currentTeacher in currentTeachers)
                {
                    if (currentTeacher.Id == originalTeacher.Id && (
                        currentTeacher.Name != originalTeacher.Name ||
                        currentTeacher.Degree != originalTeacher.Degree
                        ))
                        teachersToUpdate.Add(originalTeacher);

                    else if (!HelperMethods.CustomContains(originalTeachers, currentTeacher) &&
                            !HelperMethods.CustomContains(teachersToDelete, currentTeacher))
                        teachersToDelete.Add(currentTeacher);

                    else if (!HelperMethods.CustomContains(currentTeachers, originalTeacher) &&
                            !HelperMethods.CustomContains(teachersToAdd, originalTeacher))
                        teachersToAdd.Add(originalTeacher);
                }
            }
            //TEACHERS TO ADD
            await dBContext.Teachers.BulkInsertAsync(teachersToAdd);
            //TEACHER TO UPDATE
            foreach (var item in teachersToUpdate)
            {
                var itemToRemove = await dBContext.Teachers.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                dBContext.Teachers.Remove(itemToRemove);
                dBContext.Teachers.Add(item);
            }
            //TEACHERS TO DELETE
            dBContext.Teachers.RemoveRange(teachersToDelete);
            await dBContext.SaveChangesAsync();
        }
        public async Task<Exception> AddTeacher(int teacherId)
        {
            try
            {
                TeacherVM josnObject = new TeacherVM();
                var api = host + $"/{teacherId}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var item = await reader.ReadToEndAsync();
                    josnObject = JsonConvert.DeserializeObject<TeacherVM>(item);
                }
                dBContext.Teachers.Add(mapper.Map<Teacher>(josnObject));
                dBContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

        public Exception DeleteTeacher(int teacherId)
        {
            try
            {
                var teacherToDelete = dBContext.Teachers.Where(e => e.Id == teacherId).FirstOrDefault();
                dBContext.Teachers.Remove(teacherToDelete);
                dBContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<TeacherReource>> GetAllTeachers()
        {
            List<TeacherReource> josnObject = new List<TeacherReource>();
            var api = host;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var item = await reader.ReadToEndAsync();
                josnObject = JsonConvert.DeserializeObject<List<TeacherReource>>(item);
            }
            return josnObject;
        }

        public async Task<TeacherReource> GetTeacher(int Id)
        {
            return mapper.Map<TeacherReource>(await dBContext.Teachers.Where(e => e.Id == Id).FirstOrDefaultAsync());
        }

        public async Task<Exception> UpdateTeacher(int teacherId)
        {
            try
            {
                TeacherVM josnObject = new TeacherVM();
                var api = host + $"/{teacherId}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var item = await reader.ReadToEndAsync();
                    josnObject = JsonConvert.DeserializeObject<TeacherVM>(item);
                }
                dBContext.Teachers.Update(mapper.Map<Teacher>(josnObject));
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
