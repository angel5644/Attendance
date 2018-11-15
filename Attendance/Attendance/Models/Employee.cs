using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("Emplyee")]
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public CompanyRole CompanyRole { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset HireDate { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public virtual Student Student { get; set; }
        
        public virtual Teacher Teacher { get; set; }

        public int? ResourceManagerId { get; set; }

        [ForeignKey("ResourceManagerId")]
        public virtual Employee ResourceManager { get; set; }

        public virtual ICollection<Employee> Resources { get; set; }

        [Key]
        public int Id { get; set; }
    }

    public enum CompanyRole
    {
        Resource, 
        ResourceManager,
        Director
    }
}