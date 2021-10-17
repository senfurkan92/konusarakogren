using DAL.FluentMap;
using DOMAIN.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    /// <summary>
    /// entityframework codefirst yapısına uygun dbcontext 
    /// entityframeworkindentity sinifinin kalitilmasi ve user,role sınıfları ile konfigurasyon
    /// </summary>
    public class ProjectDbContext : IdentityDbContext<User,Role,String>
    {
        // sqlite connectionstring verilmesi
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\SqliteDBs\\konusarakogren.db;");
        }

        // fluentApi uygulanmasi
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AnswerMap());
            builder.ApplyConfiguration(new QuestionMap());
            builder.ApplyConfiguration(new QuizMap());
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
    }
}
