using Api.Data;
using Api.Models.Movie;
using Api.Models.ToDo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //User-Secrets
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //Using Secrets
            string DB_HOST = Configuration["DB_HOST"];
            string DB_DATA = Configuration["DB_DATA"];
            string DB_USER = Configuration["DB_USER"];
            string DB_PASS = Configuration["DB_PASS"];
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Register the ApplicationDbContext in DI container
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //Register the repositories in DI container
            services.AddSingleton<ITodoRepository, TodoRepository>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // this uses the policy called "default"
            app.UseCors("default");

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ScopeName = "api1",

                RequireHttpsMetadata = false
            });

            //Use Mvc
            app.UseMvc();

            //Seed Data
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}