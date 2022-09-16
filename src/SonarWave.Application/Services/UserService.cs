using AutoMapper;
using FluentValidation.Results;
using SonarWave.Application.Entities;
using SonarWave.Application.Models.User;

namespace SonarWave.Application.Services
{
    /// <summary>
    /// A service for <see cref="User"/> related operations.
    /// </summary>
    public class UserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GetAsync

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="connectionId">Represents the connection id of the user.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="User"/> if found.
        /// </returns>
        public async Task<User?> GetAsync(string connectionId)
        {
            return await _context.Users.FindAsync(connectionId);
        }

        #endregion GetAsync

        #region AddAsync

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="request">Represents the required data for adding a user.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// <see langword="true"/> if successful, otherwise <see langword="false"/>
        /// </returns>
        public async Task<bool> AddAsync(CreateUserRequest request)
        {
            CreateUserValidator validator = new CreateUserValidator();
            ValidationResult result = validator.Validate(request);

            if (!result.IsValid)
                return false;

            User user = _mapper.Map<User>(request);
            _context.Users.Add(user);

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion AddAsync

        #region RemoveAsync

        /// <summary>
        /// Removes a user.
        /// </summary>
        /// <param name="connectionId">Represents the connection id of the user.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// </returns>
        public async Task RemoveAsync(string connectionId)
        {
            User? user = await _context.Users.FindAsync(connectionId);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        #endregion RemoveAsync
    }
}