using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;
using System.Web.Mvc;

namespace Attendance.ViewModels
{
    public class CreateEmployeeVM
    {
        [Display(Name = "Resource Manager")]
        public int? ResourceManagerId { get; set; }
        public IEnumerable<SelectListItem> ResourceManagers { get; set; }


        [Display(Name = "First Name")]
        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(500)]
        public string Email { get; set; }

        [Display(Name = "Company Role")]
        public CompanyRole CompanyRole { get; set; }

        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Hire Date")]
        [Required]
        public DateTimeOffset HireDate { get; set; }

        [Display(Name = "Location")]
        [Required]
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        
    }
}