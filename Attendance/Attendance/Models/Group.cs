using Attendance.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Models
{
    [Table("Group")]
    public class Group : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public EnglishLevel Level { get; set; }

        //public virtual ICollection<Student> Students { get; set; }
    }

    
}