using Attendance.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Models
{
    [Table("Student")]
    public class Student : BaseEntity
    {
        [Key]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int Score { get; set; }

        public EnrollmentStatus EnrollmentStatus { get; set; }

        public EnglishLevel Level { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //// to be defined
        //public int? GroupId { get; set; }

        //[ForeignKey("GroupId")]
        //public virtual Group Group { get; set; }


    }

   
}