﻿using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.ViewModels
{
    public class EmployeeListVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public CompanyRole CompanyRole { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset HireDate { get; set; }

        /// <summary>
        /// The date when the entity was created
        /// </summary>
        public DateTimeOffset? DateCreated { get; set; }
    }
}