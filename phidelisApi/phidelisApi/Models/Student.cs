using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Models
{
    public class Student
    {
        [Key]
        public Guid IdStudent { get; set; }
        public string Name  { get; set; }
        
    }
}
