using Microsoft.EntityFrameworkCore;
using SonarWave.Core.Enums;
using SonarWave.Core.Extensions;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Models.File;
using SonarWave.Core.Objects;
using SonarWave.Infrastructure.Data;
using File = SonarWave.Core.Entities.File;

namespace SonarWave.Infrastructure.Services
{
    /// <summary>
    /// A service for file related operation.
    /// </summary>
    public class FileService : AbstractService, IFileService
    {
        public FileService(DatabaseContext context) : base(context)
        {
        }

        #region GetFileAsync

        public async Task<File?> GetFileAsync(string connectionId, string fileId)
        {
            if (string.IsNullOrEmpty(connectionId) || !Guid.TryParse(fileId, out _))
                return null;

            return await _context.Files.FirstOrDefaultAsync(opt => opt.Id == fileId && (opt.SenderId == connectionId || opt.RecipientId == connectionId));
        }

        #endregion GetFileAsync

        #region AddFileAsync

        public async Task<Result<File>> AddFileAsync(string connectionId, CreateFileRequest request)
        {
            if (string.IsNullOrEmpty(connectionId) || connectionId == request.RecipientId)
                return Result<File>.Failure(ErrorType.BadRequest, "Invalid connectionId.");

            var validator = new CreateFileValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return Result<File>.Failure(ErrorType.BadRequest, validationResult.ErrorMessage());

            var user = await _context.Users.FindAsync(connectionId);

            if (user == null)
                return Result<File>.Failure(ErrorType.NotFound, "User not found.");

            var recipient = await _context.Users.FindAsync(request.RecipientId);

            if (recipient == null)
                return Result<File>.Failure(ErrorType.NotFound, "Recipient not found.");

            var file = new File()
            {
                Name = request.Name,
                Path = request.Path,
                Extension = request.Extension,
                Size = request.Size,
                SenderId = user.ConnectionId,
                RecipientId = recipient.ConnectionId,
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            return Result<File>.Success(file);
        }

        #endregion AddFileAsync

        #region UpdateFileAsync

        public async Task<Result<File>> UpdateFileAsync(string connectionId, UpdateFileRequest request)
        {
            if (string.IsNullOrEmpty(connectionId))
                return Result<File>.Failure(ErrorType.BadRequest, "Invalid connectionId.");

            var validator = new UpdateFileValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return Result<File>.Failure(ErrorType.BadRequest, validationResult.ErrorMessage());

            var file = await _context.Files.FirstOrDefaultAsync(opt => opt.Id == request.Id && (opt.SenderId == connectionId || opt.RecipientId == connectionId));

            if (file == null)
                return Result<File>.Failure(ErrorType.NotFound, "File not found.");

            if (file.SenderId == connectionId)
                return Result<File>.Failure(ErrorType.BadRequest, "Unable to modify.");

            file.Acceptance = request.Acceptance;

            await _context.SaveChangesAsync();
            return Result<File>.Success(file);
        }

        #endregion UpdateFileAsync

        #region RemoveFileAsync

        public async Task<Result<bool>> RemoveFileAsync(string connectionId, string fileId)
        {
            if (string.IsNullOrEmpty(connectionId) || Guid.TryParse(fileId, out _))
                return Result<bool>.Failure(ErrorType.BadRequest, "Invalid ids.");

            var file = await _context.Files.FirstOrDefaultAsync(opt => opt.Id == fileId && opt.SenderId == connectionId);

            if (file == null)
                return Result<bool>.Failure(ErrorType.NotFound, "File not found.");

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        #endregion RemoveFileAsync
    }
}