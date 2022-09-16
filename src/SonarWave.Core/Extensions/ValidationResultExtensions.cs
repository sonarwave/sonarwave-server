using FluentValidation.Results;
using System.Text;

namespace SonarWave.Core.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ValidationResult"/>.
    /// </summary>
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Converts errors from <see cref="ValidationResult"/> to a <see langword="string"/>.
        /// </summary>
        /// <param name="result">Represents the result of a validation operation.</param>
        /// <returns>
        /// All errors as a <see langword="string"/>.
        /// </returns>
        public static string ErrorMessage(this ValidationResult result)
        {
            var errorMessage = new StringBuilder();
            foreach (var item in result.Errors)
            {
                errorMessage.AppendLine(item.ErrorMessage);
            }

            return errorMessage.ToString();
        }
    }
}