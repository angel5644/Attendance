using System;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Enums
{
    public enum EnrollmentStatus
    {
        [Display (Name = "Not Enrolled")]
        NotEnrolled,
        Enrolled,
        Exempt
    }
}