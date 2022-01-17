using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Models
{
    public class EnrolledStudent
    {
        public Guid IdEnrollment { get; set; }
        public string StudentName { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
