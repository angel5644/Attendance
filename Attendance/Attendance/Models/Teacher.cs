using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("Teacher")]
    public class Teacher : BaseEntity
    {
        [Key]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<EnglishClass> Classes { get; set; } 
    }
}