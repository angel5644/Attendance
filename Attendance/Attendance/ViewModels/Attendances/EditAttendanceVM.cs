﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;
namespace Attendance.ViewModels.Attendances
{
    public class EditAttendanceVM
    {

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public int StudentNId { get; set; }
        public IEnumerable<SelectListItem> StudentName { get; set; }


        [Display(Name = "Class Name")]
        public int ClassNId { get; set; }
        public IEnumerable<SelectListItem> ClassName { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset Date { get; set; }

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