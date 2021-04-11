using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Mappings
{
    public class UserImageMap : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.ToTable("UserImages");
            builder.HasKey(uI => new { uI.UserId, uI.ImageId });

            builder.HasOne(uI => uI.User).WithMany(u => u.UserImages).HasForeignKey(uI => uI.UserId);
            builder.HasOne(uI => uI.Image).WithMany(i => i.UserImages).HasForeignKey(uI => uI.ImageId);
        }
    }
}
