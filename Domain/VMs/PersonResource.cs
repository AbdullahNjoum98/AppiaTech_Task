using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VMs
{
    public class PersonResource
    {
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
