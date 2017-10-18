﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositoryApp.Data.Model;
using Version = RepositoryApp.Data.Model.Version;

namespace RepositoryApp.Data.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<File> Files { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User").Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Repository>().ToTable("Repository");
            builder.Entity<Version>().ToTable("Version");
            builder.Entity<File>().ToTable("File");

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUser<Guid>>();
        }
    }
}