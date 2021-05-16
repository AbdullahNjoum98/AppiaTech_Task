using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Employees")]
    public class Employee:Person
    {
        [Required]
        [Range(1000,9999)]
        public double Salary { get; set; }
    }
}
