using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attendance.Enums;


namespace Attendance.ViewModels
{
    public class DeleteStudentsVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Level")]
        public EnglishLevel Level { get; set; }

        [Display(Name = "Enrollment Status")]
        public EnrollmentStatus EnrollmentStatus { get; set; }
        
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