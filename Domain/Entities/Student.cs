using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Students")]
    public class Student:Person
    {
        [Required]
        public virtual List<Course> favCourses { get; set; }
        [NotMapped]
        public List<int> favCoursesIds { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
