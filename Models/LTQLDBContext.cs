using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace LapTrinhQuanLy.Models
{
    public partial class LTQLDBContext : DbContext
    {
        public LTQLDBContext()
            : base("name=LTQLDBContext")
        {
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.UseName)
                .IsUnicode(false);
            modelBuilder.Entity<Account>()
                .Property(e => e.PassWord)
                .IsUnicode(true);
        }
    }
}
