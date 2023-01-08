using BlogProject.Core.Map;
using BlogProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.Map
{
    public class UserConfiguration : CoreConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.EmailAddress).IsRequired(true).HasMaxLength(255);
            builder.Property(x => x.ImageURL).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(1000);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.LastIPAddress).IsRequired(false).HasMaxLength(24);

            base.Configure(builder);
        }
    }
}
