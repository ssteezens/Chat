using AutoMapper;
using Identity;
using Identity.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Services.Interfaces;

namespace Data.Services
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ChatContext _context;
		private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(ChatContext context, UserManager<User> userManager, 
            TokenConfiguration tokenConfiguration,
            IMapper mapper)
		{
			_context = context;
			_userManager = userManager;
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
			var result = await _userManager.CreateAsync(registerModel.User, registerModel.Password);

			return result;
		}

		/// <summary>
		///		Update a user.
		/// </summary>
		/// <param name="userDto"> The <see cref="UserDto"/>. </param>
		/// <returns> True or false to indicate success or failure. </returns>
        public bool UpdateUser(UserDto userDto)
        {
            var user = _context.Users.SingleOrDefault(i => i.UserName == userDto.Username);

            if (user == null)
                return false;

            user.ProfileImageData = userDto.ProfileImageData;
            user.NickName = userDto.Nickname;

            var returnValue = _context.SaveChanges() > 0;

            return returnValue;
        }

		/// <summary>
		///		Get users matching username search criteria.
		/// </summary>
		/// <param name="username"> The username part to search for. </param>
		/// <returns> Users matching username search criteria. </returns>
        public async Task<IEnumerable<UserDto>> GetUsersMatchingUsername(string username)
        {
            var users = await _context.Users.Where(i => i.UserName.StartsWith(username)).ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
