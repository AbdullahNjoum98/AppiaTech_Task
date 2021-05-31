using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotracts.VMs
{
    public class EmployeeResource:PersonResource
    {
        [Range(1000, 9999)]
        public double Salary { get; set; }
    }
}
