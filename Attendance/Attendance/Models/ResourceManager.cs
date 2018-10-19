using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("ResourceManager")]
    public class ResourceManager : BaseEntity
    {
        [Key]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<Employee> Resources { get; set; }
    }
}