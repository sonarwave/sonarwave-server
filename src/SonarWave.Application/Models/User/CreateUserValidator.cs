using FluentValidation;
using System.Net;

namespace SonarWave.Application.Models.User
{
    /// <summary>
    /// A validator for <see cref="CreateUserRequest"/> to see whether correct data has been given.
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(instance => instance.ConnectionId)
                .NotNull()
                .NotEmpty();

            RuleFor(instance => instance.RemoteIpAddress)
                .NotNull()
                .NotEmpty()
                .Must(value => IPAddress.TryParse(value, out _));

            RuleFor(instance => instance.PlatformType)
                .NotNull()
                .NotEmpty();
        }
    }
}