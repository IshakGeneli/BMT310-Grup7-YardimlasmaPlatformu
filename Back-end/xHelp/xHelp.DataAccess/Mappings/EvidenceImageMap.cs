using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Mappings
{
    public class EvidenceImageMap : IEntityTypeConfiguration<EvidenceImage>
    {
        public void Configure(EntityTypeBuilder<EvidenceImage> builder)
        {
            builder.ToTable("EvidenceImages");
            builder.HasKey(eI => new { eI.EvidenceId, eI.ImageId });

            builder.HasOne(eI => eI.Evidence).WithMany(e => e.EvidenceImages).HasForeignKey(eI => eI.EvidenceId);
            builder.HasOne(eI => eI.Image).WithMany(i => i.EvidenceImages).HasForeignKey(eI => eI.ImageId);
        }
    }
}
