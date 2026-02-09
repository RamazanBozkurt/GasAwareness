using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Entities
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
        }

        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoAgeGroup> VideoAgeGroups { get; set; }
        public DbSet<VideoCategory> VideoCategories { get; set; }
        public DbSet<VideoSubscriptionType> VideoSubscriptionTypes { get; set; }
        public DbSet<UserVideo> UserVideos { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyResult> SurveyResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserVideo>()
                .HasOne(uv => uv.User)           
                .WithMany(u => u.UserVideos)     
                .HasForeignKey(uv => uv.UserId)  
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Entity<UserVideo>()
                .HasOne(uv => uv.Video)
                .WithMany(v => v.UserVideos)     
                .HasForeignKey(uv => uv.VideoId)
                .OnDelete(DeleteBehavior.Cascade); 
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                var createdAtProperty = entityEntry.Entity.GetType().GetProperty("CreatedAt");

                if (createdAtProperty != null)
                {
                    createdAtProperty.SetValue(entityEntry.Entity, DateTime.UtcNow);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}