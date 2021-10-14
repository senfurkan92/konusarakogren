using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FluentMap
{
    public class QuizMap : BaseMap<Quiz>
    {
        public override void Configure(EntityTypeBuilder<Quiz> builder)
        {
            base.Configure(builder);
            builder.ToTable("Quizzes");
            builder.Property(x => x.ArticleContent).IsRequired();

            // relation
            builder.HasOne(x => x.User).WithMany(x => x.Quizzes).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Questions).WithOne(x => x.Quiz).HasForeignKey(x => x.QuizId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
