using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Mappings
{
    public class MissionMap : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.ToTable("Missions");
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.User).WithMany(u => u.Missions).HasForeignKey(m => m.OwnerUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.Evidences).WithOne(e => e.Mission).HasForeignKey(e => e.MissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
