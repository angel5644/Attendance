using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance.Enums;

namespace Attendance.ViewModels
{
    public class EditGroupVM
    {
        public int Id { get; set; }
        public EnglishLevel Level { get; set; }

        [Required]
        [StringLength(500)]

        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
       
    }
}