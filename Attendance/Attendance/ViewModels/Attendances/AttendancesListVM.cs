using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Attendance.ViewModels.Attendances
{
    public class AttendancesListVM
    {

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

    }
}