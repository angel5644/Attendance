using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Attendance.ViewModels.EnglisClass
{
    public class EnglishListVM
    {
        public int Id { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }

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
        public int HourStart { get; set; }

        [Display(Name = "Ends at")]
        public int HourEnd { get; set; }
    }
}