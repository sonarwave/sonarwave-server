using FluentValidation;

namespace SonarWave.Core.Models.File
{
    /// <summary>
    /// A validator for <see cref="CreateFileRequest"/> to see whether correct data has been given.
    /// </summary>
    public class CreateFileValidator : AbstractValidator<CreateFileRequest>
    {
        public CreateFileValidator()
        {
            RuleFor(opt => opt.Name)
                .NotEmpty();

            RuleFor(opt => opt.Extension)
                .NotEmpty();

            RuleFor(opt => opt.Size)
                .GreaterThan(0);

            RuleFor(opt => opt.RecipientId)
                .NotEmpty();
        }
    }
}