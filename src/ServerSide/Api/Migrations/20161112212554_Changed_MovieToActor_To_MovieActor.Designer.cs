using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Api.Data;

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161112212554_Changed_MovieToActor_To_MovieActor")]
    partial class Changed_MovieToActor_To_MovieActor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509");

            modelBuilder.Entity("Api.Models.Blog.Post", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Api.Models.Blog.PostTag", b =>
                {
                    b.Property<Guid>("PostID");

                    b.Property<string>("TagID");

                    b.HasKey("PostID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Api.Models.Blog.Tag", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Api.Models.Movie.Actor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("BornDate");

                    b.Property<string>("FirstName")
                        .HasMaxLength(60);

                    b.Property<string>("LastName")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("Api.Models.Movie.Movie", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<decimal>("Price");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Api.Models.Movie.MovieActor", b =>
                {
                    b.Property<Guid>("MovieID");

                    b.Property<Guid>("ActorID");

                    b.HasKey("MovieID", "ActorID");

                    b.HasIndex("ActorID");

                    b.ToTable("MovieActor");
                });

            modelBuilder.Entity("Api.Models.Movie.MovieReviews", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MovieID");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Api.Models.Blog.PostTag", b =>
                {
                    b.HasOne("Api.Models.Blog.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Api.Models.Blog.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Api.Models.Movie.MovieActor", b =>
                {
                    b.HasOne("Api.Models.Movie.Actor", "Actor")
                        .WithMany("MovieToActor")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Api.Models.Movie.Movie", "Movie")
                        .WithMany("MovieToActor")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Api.Models.Movie.MovieReviews", b =>
                {
                    b.HasOne("Api.Models.Movie.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
