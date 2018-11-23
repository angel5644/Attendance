using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attendance.Enums;

namespace Attendance.ViewModels
{
    public class DetailsStudentsVM
    {

        public int Id { get; set; }

        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "Score")]
        public string Score { get; set; }

        [Display(Name = "Enrollment status")]
        public EnrollmentStatus EnrollmentStatus { get; set; }

        [Display(Name = "Level")]
        public EnglishLevel Level  { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Data Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public string UserUpdated { get; set; }
    }
}