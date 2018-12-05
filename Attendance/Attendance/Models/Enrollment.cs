using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("Enrollment")]
    public class Enrollment : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public EnglishClass Class { get; set; }

        public DateTimeOffset DateEnrollment { get; set; }

        public string Notes { get; set; }

        public string StudentName
        {
            get
            {
                return Student != null ? Student.EmployeeName : string.Empty;
            }
        }

        public string ClassName
        {
            get
            {
                return Class != null ? Class.Name : string.Empty;
            }
        }
    }
}