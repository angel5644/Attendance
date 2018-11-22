using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Attendance.ViewModels
{
    public class DeleteLocationVM
    {
        public int Id { get; set; }
        [Display(Name = "Location")]
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public string Description { get; set; }
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