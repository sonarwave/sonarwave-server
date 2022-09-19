using Microsoft.EntityFrameworkCore;
using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.Extensions;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Models.User;
using SonarWave.Core.Objects;
using SonarWave.Infrastructure.Data;

namespace SonarWave.Infrastructure.Services
{
    /// <summary>
    /// A service for user related operation.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

        public UserService(IDbContextFactory<DatabaseContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        #region GetUserAsync

        public async Task<User?> GetUserAsync(string connectionId)
        {
            using DatabaseContext context = _dbContextFactory.CreateDbContext();
            return await context.Users.FindAsync(connectionId);
        }

        #endregion GetUserAsync

        #region AddUserAsync

        public async Task<Result<User>> AddUserAsync(CreateUserRequest request)
        {
            var validator = new CreateUserValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return Result<User>.Failure(ErrorType.BadRequest, validationResult.ErrorMessage());

            var user = new User()
            {
                ConnectionId = request.ConnectionId,
                RemoteIpAddress = request.RemoteIpAddress,
                PlatformType = request.PlatformType,
            };

            using DatabaseContext context = _dbContextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Result<User>.Success(user);
        }

        #endregion AddUserAsync

        #region RemoveUserAsync

        public async Task<Result<bool>> RemoveUserAsync(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
                return Result<bool>.Failure(ErrorType.BadRequest, "ConnectionId is invalid.");

            using DatabaseContext context = _dbContextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(connectionId);

            if (user == null)
                return Result<bool>.Failure(ErrorType.NotFound, "User not found.");

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        #endregion RemoveUserAsync
    }
}