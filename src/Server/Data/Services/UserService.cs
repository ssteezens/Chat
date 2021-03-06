﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Data.Services.Interfaces;
using Data.Entities;
using Data.Configuration;
using Shared.Models.Models;

namespace Data.Services
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ChatContext _context;
        private readonly UserManager<User> _userManager;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IMapper _mapper;

        public UserService(ChatContext context, UserManager<User> userManager,
            TokenConfiguration tokenConfiguration,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _tokenConfiguration = tokenConfiguration;
            _mapper = mapper;
        }

        /// <summary>
        ///		Gets a user from the database by username.
        /// </summary>
        /// <param name="username"> Username of the user. </param>
        /// <returns> User with username. </returns>
        public User GetByUsername(string username)
        {
            return _context.Users.SingleOrDefault(i => i.UserName == username);
        }

        /// <summary>
        ///		Adds a user to the database.
        /// </summary>
        /// <param name="registerModel"> The user to add. </param>
        /// <returns> The created user. </returns>
        public async Task<IdentityResult> CreateUserAsync(RegisterModel registerModel)
        {
            var userEntity = _mapper.Map<User>(registerModel.User);
            var result = await _userManager.CreateAsync(userEntity, registerModel.Password);

            return result;
        }

        /// <summary>
        ///		Create a Jwt bearer token for the user.
        /// </summary>
        /// <param name="user"> User to create token for. </param>
        /// <returns> Jwt bearer token for user. </returns>
        public string CreateToken(User user)
        {
            // create token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _tokenConfiguration.Issuer,
                _tokenConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials);
            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return results.token;
        }

        /// <summary>
        ///		Update a user.
        /// </summary>
        /// <param name="userModel"> The <see cref="UserModel"/>. </param>
        /// <returns> True or false to indicate success or failure. </returns>
        public bool UpdateUser(UserModel userModel)
        {
            var user = _context.Users.SingleOrDefault(i => i.UserName == userModel.Username);

            if (user == null)
                return false;

            user.ProfileImageData = userModel.ProfileImageData;
            user.NickName = userModel.Nickname;

            var returnValue = _context.SaveChanges() > 0;

            return returnValue;
        }

        /// <summary>
        ///		Get users matching username search criteria.
        /// </summary>
        /// <param name="username"> The username part to search for. </param>
        /// <returns> Users matching username search criteria. </returns>
        public async Task<IEnumerable<UserModel>> GetUsersMatchingUsername(string username)
        {
            var users = await _context.Users.Where(i => i.UserName.StartsWith(username)).ToListAsync();

            return _mapper.Map<List<UserModel>>(users);
        }
    }
}
