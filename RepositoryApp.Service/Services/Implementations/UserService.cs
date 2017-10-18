﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryApp.Data.DAL;
using RepositoryApp.Data.Model;
using RepositoryApp.Service.Services.Interfaces;

namespace RepositoryApp.Service.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == userId);
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task<bool> UserExist(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == userId) != null;
        }

        public TokenModel GenerateTokenForUser(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var resultToken = new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

            return resultToken;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
