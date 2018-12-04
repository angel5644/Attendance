﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;

namespace Attendance.ViewModels.EnglisClass
{
    public class DetailsEnglishVM
    {
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Monday")]
        public bool IsMonday { get; set; }

        [Display(Name = "Tuesday")]
        public bool IsTuesday { get; set; }

        [Display(Name = "Wednesday")]
        public bool IsWednesday { get; set; }

        [Display(Name = "Thursday")]
        public bool IsThursday { get; set; }

        [Display(Name = "Friday")]
        public bool IsFriday { get; set; }

        [Display(Name = "Starts at")]
        [Required]
        [Range(0, 23)]
        public int HourStart { get; set; }

        [Display(Name = "Ends at")]
        [Required]
        [Range(0, 23)]
        public int HourEnd { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Upddated")]
        public string UserUpdated { get; set; }





    }
}