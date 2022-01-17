using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Models
{
    public class Enrollment
    {
        [Key]
        public Guid IdEnrollment { get; set; }
        [ForeignKey("Student")]
        public Guid IdStudent { get; set; }
        public bool Active { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
