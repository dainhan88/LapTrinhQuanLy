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

        public virtual DbSet<Person> People { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(e => e.PersonID)
                .IsUnicode(false);
            modelBuilder.Entity<Person>()
                .Property(e => e.PersonName)
                .IsUnicode(false);
        }
    }
}
