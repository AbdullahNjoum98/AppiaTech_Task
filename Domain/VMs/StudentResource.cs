using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VMs
{
    public class StudentResource:PersonResource
    {
        [Required]
        public List<FavCourseResource> favCourses { get; set; }
        public TeacherReource teacher { get; set; }

    }
}
