using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Contact).WithOne(c => c.User).HasForeignKey<Contact>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Achievements).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Missions).WithOne(m => m.User).HasForeignKey(m => m.OwnerUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
