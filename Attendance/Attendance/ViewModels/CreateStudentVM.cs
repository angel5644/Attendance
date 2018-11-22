﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;


namespace Attendance.ViewModels
{
    public class CreateStudentVM
    {
        [Display(Name = "Employee Id")]
        [Required] 
        public int EmployeeId { get; set; }

        [Display(Name = "Score")]
        [Required]
        public int Score { get; set; }

        [Display(Name = "Enrollment Status")]
        [Required]
        public EnrollmentStatus EnrollmentStatus { get; set; }

        [Display(Name = "Level")]
        [Required]
        public EnglishLevel Level { get; set; }

        [Display(Name = "Date Created")]
        [Required]
        public DateTimeOffset?  DateCreated  { get; set; }

        [Display(Name = "User Created")]
        [Required]
        public string UserCreated { get; set; }

        [Display(Name = "Date Updated")]
        [Required]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Upddated")]
        [Required]
        public String UserUpdated { get; set; }

    }
}