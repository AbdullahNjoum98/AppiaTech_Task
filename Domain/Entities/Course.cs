using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string Code { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Name { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
