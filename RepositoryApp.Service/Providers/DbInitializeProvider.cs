﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RepositoryApp.Data.DAL;
using RepositoryApp.Data.Model;
using RepositoryApp.Service.Helpers;
using Version = RepositoryApp.Data.Model.Version;

namespace RepositoryApp.Service.Providers
{
    public class DbInitializeProvider
    {
        public static void InitializeWithDefaults(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            if (dbContext.Users.Any())
                return;
            var random = string.Empty;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[32]);
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("1qaz@WSX", salt),
                    UniqueName = $"tescik{random.RandomString(10)}",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Salt = salt,
                    CreationDateTime = DateTime.Now,
                    Path = "",
                    Repositories = new List<Repository>
                    {
                        new Repository
                        {
                            Id = Guid.NewGuid(),
                            Name = "MyRepository1",
                            UniqueName = $"MyRepository1_{random.RandomString(10)}",
                            CreationDateTime = DateTime.Now,
                            Path = "",
                            Versions = new List<Version>
                            {
                                new Version
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "1",
                                    UniqueName = $"1_{random.RandomString(10)}",
                                    Description = "For Test",
                                    CreationDateTime = DateTime.Now,
                                    Path = "",
                                    Files = new List<File>
                                    {
                                        new File
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = "FileOne",
                                            CreationDateTime = DateTime.Now,
                                            Path = "",
                                            ContentType = ""
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}