using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;
using Attendance.ViewModels;

namespace Attendance.ViewModels.Enrollment
{
    public class DetailsEnrollmentVM
        // Ray//
  
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

        [Display(Name = "Date created")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Upddated")]
        public string UserUpdated { get; set; }



    }
}