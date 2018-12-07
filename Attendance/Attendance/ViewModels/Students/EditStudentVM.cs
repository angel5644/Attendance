using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.ViewModels.Students
{
    public class EditStudentVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Student´s Name")]
        [UIHint ("stname")]
        public string stname { get; set; }

        [Display (Name = "Name")]
        public int EmployeeId { get; set; }
        public IEnumerable<SelectListItem> Name { get; set; }

        [Display (Name = "Score")]
        public int Score { get; set; }

        [Display (Name = "Enrollment Status")]
        public EnrollmentStatus EnrollmentStatus { get; set; }

        [Display (Name = "Level")]
        public EnglishLevel Level { get; set; }

        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
    }
}