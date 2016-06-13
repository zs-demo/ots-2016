﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .HasMany(i => i.Genres)
                .WithOne(i => i.Movie)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Movie> TblMovies { get; set; }
        public DbSet<MovieWithGenre> TblMoviesWithGenres { get; set; }
    }
}
