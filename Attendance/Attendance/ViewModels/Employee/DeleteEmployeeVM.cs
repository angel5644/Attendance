using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.ViewModels
{
    public class DeleteEmployeeVM
    {
        public int Id { get; set; }

        [Display(Name = "Location Name")]
        [StringLength(500)]
        public string LocationName { get; set; }

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

        [Display(Name = "Is Enable")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? HireDate { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateCreated { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }
        [Display(Name = "Data Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset? DateUpdated { get; set; }

        [Display(Name = "User Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public string UserUpdated { get; set; }

        [Display(Name = "Resource Manager")]
        [StringLength(500)]
        public string ResourceManagerName { get; set; }
    }
}