using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("EnglishClass")]
    public class EnglishClass : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        //public int TeacherId { get; set; }

        //[ForeignKey("TeacherId")]
        //public Teacher Teacher { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Employee Teacher { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        //public DateTimeOffset Date { get; set; }

        // Schedule
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }

        [Required]
        [Range(0, 23)]
        public int HourStart { get; set; }

        [Required]
        [Range(0, 23)]
        public int HourEnd { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        [NotMapped]
        public string TeacherName
        {
            get
            {
                return Teacher != null ? Teacher.FirstName + " " + Teacher.LastName : string.Empty;
            }
        }
        public string GroupName
        {
            get
            {
                return Group != null ? Group.Name : string.Empty;
            }
        }
        public string LocationName
        {
            get
            {
                //return Location.Name;
                return Location != null ? Location.Name : string.Empty;
            }
        }
    }
}