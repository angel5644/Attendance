using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class CreateEmployeeVM
    {
        public CompanyRole companypRole { get; set; }
        [Required]
        [StringLength (500)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string EMail { get; set; }
                     
    }
}