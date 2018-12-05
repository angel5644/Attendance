using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Attendance.ViewModels
{
    public class EnrollmentListVM
    {
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTimeOffset DateEnrollment { get; set; }

        [Display(Name = "Note")]
        public string Notes { get; set; }

    }
}