using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Students")]
    public class Student:Person
    {
        [Required]
        public Course favCourse { get; set; }
    }
}
