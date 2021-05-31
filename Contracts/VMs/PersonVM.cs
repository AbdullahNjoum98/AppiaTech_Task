using Contracts.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotracts.VMs
{
    public class PersonVM
    {
        public Int64 Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$")]
        [MaxLength(50)]
        [PhoneEmailValidator(new string[] { "Phone" })]
        public string Email { get; set; }
        [RegularExpression(@"^[0-9]{10}$")]
        [PhoneEmailValidator(new string[] { "Email" })]
        public string Phone { get; set; }
    }
}
