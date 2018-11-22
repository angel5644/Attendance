using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class GroupListVM
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Level")]
        [Required]
        public EnglishLevel Level { get; set; }

        [Display(Name = "Date Created")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "User Updated")]
        public string UserUpdated { get; set; }

        public int Id { get; set; }


    }
}