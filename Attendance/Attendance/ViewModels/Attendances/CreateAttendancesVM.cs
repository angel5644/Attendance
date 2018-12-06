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
    public class CreateAttendancesVM
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

        [Display(Name = "Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

    }
}