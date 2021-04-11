using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.DataAccess.Mappings;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class xHelpDbContext : IdentityDbContext<User, UserRole, string>
    {

        public xHelpDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL5103.site4now.net;Initial Catalog=DB_A71B8C_xHelp;User Id=DB_A71B8C_xHelp_admin;Password=P#dR4i3!SVsBifT");
        }
        public xHelpDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new MissionMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserImageMap());
            modelBuilder.ApplyConfiguration(new MissionImageMap());
            modelBuilder.ApplyConfiguration(new EvidenceImageMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<MissionImage> MissionImages { get; set; }
        public DbSet<EvidenceImage> EvidenceImages { get; set; }

    }
}
