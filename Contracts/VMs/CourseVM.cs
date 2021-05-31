using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotracts.VMs
{
    public class FavCourseVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
