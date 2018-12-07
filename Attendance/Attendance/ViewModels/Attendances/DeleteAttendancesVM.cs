using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;

namespace Attendance.ViewModels.Attendances
{
    public class DeleteAttendancesVM
    {

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string SName { get; set; }

        [Display(Name = "Class Name")]
        public string CName { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Updated")]
        public string UserUpdated { get; set; }
    }
}