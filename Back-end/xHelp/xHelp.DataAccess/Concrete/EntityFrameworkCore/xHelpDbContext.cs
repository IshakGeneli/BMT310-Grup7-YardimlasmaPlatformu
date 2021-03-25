using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.DataAccess.Mappings;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class xHelpDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=xHelp; Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new MissionMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
    }
}
