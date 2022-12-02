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

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
