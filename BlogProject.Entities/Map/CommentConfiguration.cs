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
    public class CommentConfiguration :CoreConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.Property(x => x.CommentText).IsRequired(true).HasMaxLength(500);

            base.Configure(builder);
        }

    }
}
