using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Attendance.ViewModels.EnglisClass
{
    public class DeleteEnglishVM
    {
        public int Id { get; set; }

        [Display (Name = "Group")]
        public string Group { get; set; }

        [Display (Name = "Location")]
        public string Location { get; set; }

        [Display (Name = "Teacher")]
        public string Teacher { get; set; }

        [Display (Name = "Monday")]
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

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "User Updated")]
        public string UserUpdated { get; set; }
    }
}