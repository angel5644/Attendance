using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Attendance.Enums;

namespace Attendance.ViewModels
{
    public class CreateEmployeeVM
    {
        
        [Required]
        [StringLength (500)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string EMail { get; set; }
                     
    }
}