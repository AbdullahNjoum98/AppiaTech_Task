using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotracts.VMs
{
    public class StudentResource:PersonResource
    {
        [Required]
        public List<FavCourseResource> favCourses { get; set; }
        public TeacherReource teacher { get; set; }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                StudentResource p = (StudentResource)obj;
                return Id == p.Id
                && Name == p.Name
                && Phone == p.Phone
                && Email == p.Email
                && teacher.Equals(p.teacher)
                && favCourses.SequenceEqual(p.favCourses);
            }
        }


    }
}
