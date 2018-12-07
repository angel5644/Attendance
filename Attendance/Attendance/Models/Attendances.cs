using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    [Table("Attendance")]
    public class Attendances : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual EnglishClass Class { get; set; }
        
        public DateTimeOffset Date { get; set; }
        
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