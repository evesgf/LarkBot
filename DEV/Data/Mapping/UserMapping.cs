using Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class UserMapping:IMapping
    {
        public UserMapping(EntityTypeBuilder<SysUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Password).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.NickName).IsRequired().HasMaxLength(50);
        }
    }
}
