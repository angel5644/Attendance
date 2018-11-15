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
        
        [Required]
        [StringLength (500)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }

        public CompanyRole CompanyRole { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset HireDate { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        public IEnumerable<SelectListItem> ResourceManagers { get; set; }
    }
}