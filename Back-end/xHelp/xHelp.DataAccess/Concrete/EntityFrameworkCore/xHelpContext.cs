using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class xHelpContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TaskMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
    }
}
