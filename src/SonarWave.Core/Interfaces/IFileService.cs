using SonarWave.Core.Enums;
using SonarWave.Core.Models.File;
using SonarWave.Core.Objects;

namespace SonarWave.Core.Interfaces
{
    /// <summary>
    /// An interface for file related operations.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Used for getting a <see cref="Entities.File"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the connected user.</param>
        /// <param name="fileId">Represents the id of the <see cref="Entities.File"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Entities.File"/>, if found.
        /// </returns>
        public Task<Entities.File?> GetFileAsync(string connectionId, string fileId);

        /// <summary>
        /// Used for adding a <see cref="Entities.File"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the connected user.</param>
        /// <param name="request">Represents the required data for adding a <see cref="Entities.File"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{Entities.File}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// <item><see cref="ErrorType.NotFound"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<Entities.File>> AddFileAsync(string connectionId, CreateFileRequest request);

        /// <summary>
        /// Used for updating a <see cref="Entities.File"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the connected user.</param>
        /// <param name="request">Represents the required data for updating a <see cref="Entities.File"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{Entities.File}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// <item><see cref="ErrorType.NotFound"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<Entities.File>> UpdateFileAsync(string connectionId, UpdateFileRequest request);

        /// <summary>
        /// Used for deleting a <see cref="Entities.File"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the connected user.</param>
        /// <param name="fileId">Represents the id of the <see cref="Entities.File"/>.</param>
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
        public Task<Result<bool>> RemoveFileAsync(string connectionId, string fileId);
    }
}