using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;
namespace Attendance.ViewModels.EnglisClass
{
    public class CreateEnglishVM
    {
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Teacher's Name")]
        [Required]
        public int EmployeeId { get; set; }
        [StringLength (500)]
        public IEnumerable<SelectListItem> TName { get; set; }

        [Display(Name = "Location's Name")]
        [Required]
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> LName { get; set; }

        [Display(Name = "Group's Name")]
        [Required]
        public int GroupId { get; set; }
        public IEnumerable<SelectListItem> GName { get; set; }

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

        //[Display(Name = "Date Created")]

        public DateTimeOffset? DateCreated { get; set; }

        //[Display(Name = "User Created")]
        ////[Required]
        public string UserCreated { get; set; }

        //[Display(Name = "Date Updated")]
        //[Required]
        public DateTimeOffset? DateUpdated { get; set; }

        //[Display(Name = "User Upddated")]
        //
[Required]
        public string UserUpdated { get; set; }
    }
}