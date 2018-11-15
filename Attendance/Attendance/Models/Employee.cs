using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("Employee")]
    public class Employee : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public CompanyRole CompanyRole { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset? HireDate { get; set; }
        [Required]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public virtual Student Student { get; set; }
        
        public virtual Teacher Teacher { get; set; }

        public int? ResourceManagerId { get; set; }

        [ForeignKey("ResourceManagerId")]
        public virtual Employee ResourceManager { get; set; }

        public virtual ICollection<Employee> Resources { get; set; }

        public string ResourceManagerName { get
            {
                return ResourceManager != null ? ResourceManager.FirstName + " " + ResourceManager.LastName : string.Empty;
            }
        }
    }

    public enum CompanyRole
    {
        Resource, 
        ResourceManager,
        Director
    }
}