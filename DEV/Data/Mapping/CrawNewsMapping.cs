using System;
using System.Collections.Generic;
using System.Text;
using Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class CrawNewsMapping:IMapping
    {
        public CrawNewsMapping(EntityTypeBuilder<CrawlNews> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Id).IsRequired();
            entityBuilder.Property(x => x.Title).IsRequired().HasMaxLength(128);
            entityBuilder.Property(x => x.ImportantLevel).IsRequired();
            entityBuilder.Property(x => x.From).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.FromUrl).HasMaxLength(512);
            entityBuilder.Property(x => x.PushTime).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Content).HasMaxLength(4096);
            entityBuilder.Property(x => x.Tag).HasMaxLength(50);
            entityBuilder.Property(x => x.PushLevel).IsRequired();
            entityBuilder.Property(x => x.AddTime).IsRequired().HasMaxLength(50);
        }
    }
}
