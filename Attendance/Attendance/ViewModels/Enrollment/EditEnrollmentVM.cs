using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;
namespace Attendance.ViewModels.Enrollment
{
    public class EditEnrollmentVM
    {
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [Required]
        public int StId { get; set; }
        public IEnumerable<SelectListItem> StudentName { get; set; }

        [Display(Name = "Class Name")]
        [Required]
        public int ClId { get; set; }
        public IEnumerable<SelectListItem> ClassName { get; set; }

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset DateEnrollment { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Date created")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Upddated")]
        public string UserUpdated { get; set; }

    }
}