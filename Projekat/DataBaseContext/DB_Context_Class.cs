using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    public class DB_Context_Class : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<WorkPackage> WorkPackage { get; set; }
        public DbSet<DatabaseEntityLib.Task> Task { get; set; }
        public DbSet<DatabaseEntityLib.Activity> Activity { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public IEnumerable<object> Tasks { get; internal set; }


        public DB_Context_Class(DbContextOptions<DB_Context_Class> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .IsRequired();

            // WorkPackage
            modelBuilder.Entity<WorkPackage>()
                .Property(wp => wp.Name)
                .IsRequired();

            modelBuilder.Entity<WorkPackage>()
                .Property(wp => wp.Priority)
                .HasConversion<string>()
                .HasMaxLength(6)
                .HasDefaultValue("Low");

            modelBuilder.Entity<WorkPackage>()
                .HasOne(wp => wp.Project)   
                .WithMany(p => p.WorkPackages)  
                .HasForeignKey(wp => wp.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            // Task
            modelBuilder.Entity<DatabaseEntityLib.Task>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<DatabaseEntityLib.Task>()
                .Property(t => t.Deadline)
                .IsRequired();

            modelBuilder.Entity<DatabaseEntityLib.Task>()
                .Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<DatabaseEntityLib.Task>()
                .HasOne(t => t.WorkPackage)
                .WithMany(wp => wp.Tasks)
                .HasForeignKey(t => t.WorkPackageID)
                .OnDelete(DeleteBehavior.Cascade);

            // Activity
            modelBuilder.Entity<DatabaseEntityLib.Activity>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<DatabaseEntityLib.Activity>()
                .Property(a => a.DatePerformed)
                .IsRequired();

            modelBuilder.Entity<DatabaseEntityLib.Activity>()
                .HasOne(a => a.Task)
                .WithMany(t => t.Activities)
                .HasForeignKey(a => a.TaskID)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Phone)
                .IsUnique();

            // Team
            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .IsRequired();

            // TeamMember
            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Employee)
                .WithMany(e => e.TeamMembers)
                .HasForeignKey(tm => tm.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            // Assignment
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Assignments)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Activity)
                .WithMany(act => act.Assignments)
                .HasForeignKey(a => a.ActivityID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Month)
                .IsRequired();

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Year)
                .IsRequired();

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Progress)
                .IsRequired();

            modelBuilder.Entity<Assignment>()
                .Property(a => a.AssignedDays)
                .HasDefaultValue(0);
        }

    }
}
