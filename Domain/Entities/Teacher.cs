using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string Degree { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
