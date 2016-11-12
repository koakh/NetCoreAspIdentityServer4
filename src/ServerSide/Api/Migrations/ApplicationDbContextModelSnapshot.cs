﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Api.Data;

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509");

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

                    b.Property<Guid?>("ActorID");

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

                    b.HasIndex("ActorID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Api.Models.Movie.MovieReviews", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MovieID");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Api.Models.Movie.Movie", b =>
                {
                    b.HasOne("Api.Models.Movie.Actor")
                        .WithMany("ActInMovies")
                        .HasForeignKey("ActorID");
                });

            modelBuilder.Entity("Api.Models.Movie.MovieReviews", b =>
                {
                    b.HasOne("Api.Models.Movie.Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID");
                });
        }
    }
}
