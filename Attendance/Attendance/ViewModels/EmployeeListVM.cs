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
    public class EmployeeListVM
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Company Role")]
        public CompanyRole CompanyRole { get; set; }
        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }
        [Display(Name = "Hire Date")]
        public DateTimeOffset? HireDate { get; set; }
        [Display(Name = "Locations Name")]
        public string LocationName { get; set; }
        [Display(Name = "Resource Manager")]
        public string ResourceManagerName { get; set; }


        /// <summary>
        /// The date when the entity was created
        /// </summary>
        /// 
        public DateTimeOffset? DateCreated { get; set; }
    }
}