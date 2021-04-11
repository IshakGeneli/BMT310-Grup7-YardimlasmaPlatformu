using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Mappings
{
    public class MissionImageMap : IEntityTypeConfiguration<MissionImage>
    {
        public void Configure(EntityTypeBuilder<MissionImage> builder)
        {
            builder.ToTable("MissionImages");
            builder.HasKey(mI => new { mI.MissionId, mI.ImageId });

            builder.HasOne(mI => mI.Mission).WithMany(m => m.MissionImages).HasForeignKey(mI => mI.MissionId);
            builder.HasOne(mI => mI.Image).WithMany(i => i.MissionImages).HasForeignKey(mI => mI.ImageId);
        }
    }
}
