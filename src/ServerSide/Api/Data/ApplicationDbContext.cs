using Api.Models.Blog;
using Api.Models.Movie;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Movie
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<MovieReviews> Reviews { get; set; }
        //Blog
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Movie
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

            //Blog
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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseMySql((@"server=localhost;database=IdentityServer4Api;uid=root;pwd=XXXXXX"));
    }
}
