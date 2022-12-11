using Microsoft.EntityFrameworkCore;
using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL
{
    public class EfCoreDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().HasIndex(s => s.Login).IsUnique();
            modelBuilder.Entity<Student>().Property(s => s.FirstName).IsRequired().HasMaxLength(25);      
            modelBuilder.Entity<Student>().Property(s => s.LastName).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Student>().Property(s => s.Age).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Room).IsRequired();

            modelBuilder.Entity<Job>().HasKey(j => j.Id);
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Student)
                .WithMany(s => s.Jobs)
                .HasForeignKey(j => j.StudentId);
            modelBuilder.Entity<Job>().Property(s => s.Title).IsRequired().HasMaxLength(30);       
            modelBuilder.Entity<Job>().Property(s => s.Definition).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Job>().Property(s => s.StartDate).IsRequired();
            modelBuilder.Entity<Job>().Property(s => s.ExpirationDate).IsRequired();
            modelBuilder.Entity<Job>().Property(s => s.JobStatus).IsRequired();

        }
    }
}
