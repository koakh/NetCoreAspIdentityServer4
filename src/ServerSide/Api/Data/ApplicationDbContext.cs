using Api.Models;
using Api.Models.Blog;
using Api.Models.Movie;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Movie
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<MovieReview> Reviews { get; set; }
        //Blog
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseMySql((@"server=localhost;database=IdentityServer4Api;uid=root;pwd=XXXXXX"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationships : Movie
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostID, t.TagID });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostID);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagID);

            //Relationships : Blog
            modelBuilder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieID, x.ActorID });

            modelBuilder.Entity<MovieActor>()
                .HasOne(pt => pt.Movie)
                .WithMany(p => p.MovieActors)
                .HasForeignKey(pt => pt.MovieID);

            modelBuilder.Entity<MovieActor>()
                .HasOne(pt => pt.Actor)
                .WithMany(t => t.MovieActors)
                .HasForeignKey(pt => pt.ActorID);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.Current.User.Identity.Name
            //    : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                    //((BaseEntity)entity.Entity).UserCreated = currentUsername;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                //((BaseEntity)entity.Entity).UserModified = currentUsername;
            }
        }
    }
}
