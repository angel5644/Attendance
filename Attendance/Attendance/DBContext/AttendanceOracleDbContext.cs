using Attendance.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Attendance.DBContext
{
    public class AttendanceOracleDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public System.Data.Entity.DbSet<Attendance.Models.Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ATTENDANCE");

        
            //modelBuilder.Entity<GroupGoal>()
            //            .HasOptional(e => e.Focus)
            //            .WithMany()
            //            .HasForeignKey(e => e.FocusId)
            //            .WillCascadeOnDelete();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            #region relationships
            // Employee 1:0..N Location
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Employees)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                ;

            // Student 0..1:1 Employee
            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.Student)
                .WithRequired(s => s.Employee)
                .WillCascadeOnDelete();

            // ResourceManager - Employee
            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.ResourceManager)
                .WithMany(e => e.Resources)
                .HasForeignKey(e => e.ResourceManagerId);

            //// Teacher 0..1:1 Employee
            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.Teacher)
            //    .WithRequired(t => t.Employee)
            //    .WillCascadeOnDelete();

            // Student 1:0..N Attendance
            modelBuilder.Entity<Student>()
                .HasMany<Attendance.Models.Attendance>(s => s.Attendances)
                .WithRequired(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .WillCascadeOnDelete();

            // EnglishClass 1:0..N Attendance
            modelBuilder.Entity<EnglishClass>()
                .HasMany<Attendance.Models.Attendance>(ec => ec.Attendances)
                .WithRequired(a => a.Class)
                .HasForeignKey(a => a.ClassId)
                .WillCascadeOnDelete();

            // EnglishClass 1:0..N Enrollment
            modelBuilder.Entity<EnglishClass>()
                .HasMany<Enrollment>(ec => ec.Enrollments)
                .WithRequired(a => a.Class)
                .HasForeignKey(a => a.ClassId)
                .WillCascadeOnDelete();

            // Student 1:0..N Enrollment
            modelBuilder.Entity<Student>()
                .HasMany<Enrollment>(s => s.Enrollments)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            // Location 1:0..N EnglishClass
            modelBuilder.Entity<Location>()
                .HasMany<EnglishClass>(l => l.Classes)
                .WithRequired(ec => ec.Location)
                .HasForeignKey(ec => ec.LocationId);

            // Location 1:0..N EnglishClass
            modelBuilder.Entity<Location>()
                .HasMany<Employee>(l => l.Employees)
                .WithRequired(e => e.Location)
                .HasForeignKey(ec => ec.LocationId);
            #endregion
        }

        public AttendanceOracleDbContext()
            : base("AttendanceOracleConnection")
        {
            
        }

        public static AttendanceOracleDbContext Create()
        {
            return new AttendanceOracleDbContext();
        }

        public System.Data.Entity.DbSet<Attendance.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Attendance.Models.Student> Students { get; set; }

        //public System.Data.Entity.DbSet<Attendance.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<Attendance.Models.Attendance> Attendances { get; set; }

        public System.Data.Entity.DbSet<Attendance.Models.EnglishClass> EnglishClasses { get; set; }

        public System.Data.Entity.DbSet<Attendance.Models.Enrollment> Enrollments { get; set; }
    }
}