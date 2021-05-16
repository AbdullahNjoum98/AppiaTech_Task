using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("People")]
    public class Person
    {
        [Key]
        public Int64 Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@" ^ ([\w\.\-] +)@([\w\-] +)((\.(\w){2, 3})+)$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string Phone { get; set; }

    }
}
