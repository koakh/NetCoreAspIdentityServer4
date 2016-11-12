using Api.Models.Movie;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Create database
                context.Database.EnsureCreated();

                //Init Faker
                Faker faker = new Faker();
                DateTime bornDate;

                // Look for any actors.
                if (!context.Actor.Any())
                {
                    bornDate = faker.Date.Past(50);
                    Actor actorFake1 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year//,
                        //ActInMovies = context.Movie.Where(s => s.Title.Contains("Bravo")).ToList()
                    };

                    bornDate = faker.Date.Past(52);
                    Actor actorFake2 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year
                    };

                    bornDate = faker.Date.Past(54);
                    Actor actorFake3 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year
                    };

                    bornDate = faker.Date.Past(54);
                    Actor actorFake4 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year
                    };

                    bornDate = faker.Date.Past(56);
                    Actor actorFake5 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year
                    };

                    bornDate = faker.Date.Past(56);
                    Actor actorFake6 = new Actor
                    {
                        FirstName = faker.Name.FirstName(Name.Gender.Male),
                        LastName = faker.Name.LastName(Name.Gender.Male),
                        BornDate = bornDate,
                        Age = DateTime.Now.Year - bornDate.Year
                    };

                    context.Actor.AddRange(
                        actorFake1, actorFake2, actorFake3, actorFake4, actorFake5, actorFake6,
                        new Actor
                        {
                            FirstName = "Laurence",
                            LastName = "Fishburne",
                            BornDate = new DateTime(1961, 6, 30),
                            Age = DateTime.Now.Year - 1961
                        }
                     );

                    // Look for any movies.
                    if (!context.Movie.Any())
                    {
                        context.Movie.AddRange(
                            new Movie
                            {
                                Title = "When Harry Met Sally",
                                ReleaseDate = DateTime.Parse("1989-1-11"),
                                Genre = "Romantic Comedy",
                                Price = 7.99M,
                                Rating = "C",
                                //Actors = new List<Actor>()
                                //{
                                //    actorFake1, actorFake2
                                //},
                                Reviews = new List<MovieReviews>()
                                {
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() },
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() }
                                }
                            },

                            new Movie
                            {
                                Title = "Ghostbusters",
                                ReleaseDate = DateTime.Parse("1984-3-13"),
                                Genre = "Comedy",
                                Price = 8.99M,
                                Rating = "A",
                                //Actors = new List<Actor>()
                                //{
                                //    actorFake2, actorFake3
                                //},
                                Reviews = new List<MovieReviews>()
                                {
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() },
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() }
                                }
                            },

                            new Movie
                            {
                                Title = "Ghostbusters 2",
                                ReleaseDate = DateTime.Parse("1986-2-23"),
                                Genre = "Comedy",
                                Price = 9.99M,
                                Rating = "B",
                                //Actors = new List<Actor>()
                                //{
                                //    actorFake3, actorFake4
                                //},
                                Reviews = new List<MovieReviews>()
                                {
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() },
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() }
                                }
                            },

                            new Movie
                            {
                                Title = "Rio Bravo",
                                ReleaseDate = DateTime.Parse("1959-4-15"),
                                Genre = "Western",
                                Price = 3.99M,
                                Rating = "D",
                                //Actors = new List<Actor>()
                                //{
                                //    actorFake4, actorFake5
                                //},
                                Reviews = new List<MovieReviews>()
                                {
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() },
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() }
                                }
                            },

                            new Movie
                            {
                                Title = "Mask",
                                ReleaseDate = DateTime.Parse("1995-4-15"),
                                Genre = "Comedy",
                                Price = 2.99M,
                                Rating = "D",
                                //Actors = new List<Actor>()
                                //{
                                //    actorFake5, actorFake6
                                //},
                                Reviews = new List<MovieReviews>()
                                {
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() },
                                    new MovieReviews() { Title = faker.Lorem.Word(), Review = faker.Lorem.Sentence() }
                                }
                            }
                        );
                    }
                }

                // Look for any reviews
                if (context.Reviews.Any())
                {
                    context.Reviews.AddRange(
                        new MovieReviews
                        {
                            Movie = context.Movie.First(s => s.Title.Contains("When Harry Met Sally")),
                            Title = faker.Lorem.Word(),
                            Review = faker.Lorem.Sentence()
                        }
                    );
                }

                //Finish Seed Saving Data
                context.SaveChanges();
            }
        }
    }
}