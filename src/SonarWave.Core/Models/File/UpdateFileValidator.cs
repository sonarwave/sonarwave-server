using FluentValidation;
using SonarWave.Core.Enums;

namespace SonarWave.Core.Models.File
{
    /// <summary>
    /// A validator for <see cref="UpdateFileRequest"/> to see whether correct data has been given.
    /// </summary>
    public class UpdateFileValidator : AbstractValidator<UpdateFileRequest>
    {
        public UpdateFileValidator()
        {
            RuleFor(opt => opt.Id)
                .NotEmpty()
                .Must(value => Guid.TryParse(value, out _));

            RuleFor(opt => opt.Acceptance)
                .NotNull()
                .NotEqual(TransferAcceptance.Initial);
        }
    }
}