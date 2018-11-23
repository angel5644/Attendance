using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attendance.Enums;

namespace Attendance.ViewModels
{
    public class StudentsListVM
    {

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EName { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Level")]
        public EnglishLevel Level { get; set; }

        [Display(Name = "Enrollment Status")]
        public EnrollmentStatus EnrollmentStatus { get; set; }

    }
}