using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotracts.VMs
{
    public class TeacherReource
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string Degree { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TeacherReource p = (TeacherReource)obj;
                return Id == p.Id
                    && Name == p.Name
                    && Degree == p.Degree;
            }
        }
    }
}
