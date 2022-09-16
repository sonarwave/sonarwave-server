using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.Models.User;
using SonarWave.Core.Objects;

namespace SonarWave.Core.Interfaces
{
    /// <summary>
    /// An interface for user related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Used for getting a <see cref="User"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the <see cref="User"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation,
        /// a <see cref="User"/>.
        /// </returns>
        public Task<User?> GetUserAsync(string connectionId);

        /// <summary>
        /// Used for adding a <see cref="User"/>.
        /// </summary>
        /// <param name="request">Represents the required data for adding a <see cref="User"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{User}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<User>> AddUserAsync(CreateUserRequest request);

        /// <summary>
        /// Used for deleting a <see cref="User"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the <see cref="User"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{bool}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// <item><see cref="ErrorType.NotFound"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<bool>> DeleteUserAsync(string connectionId);
    }
}