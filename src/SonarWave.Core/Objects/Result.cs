using SonarWave.Core.Enums;

namespace SonarWave.Core.Objects
{
    /// <summary>
    /// Represents a model which can be used when executing different form
    /// of operations. It contains detailes about how the operation went.
    /// </summary>
    /// <typeparam name="T">Represents the data returned from the operation.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Represents whether an operation was a success or not.
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// The data returned from the operation.
        /// </summary>
        public T Value { get; private set; } = default!;

        /// <summary>
        /// Used for providing detailes about an operation. Incase of failure.
        /// </summary>
        public ResultFailure Fault { get; private set; } = default!;

        /// <summary>
        /// Represents that the operation was a success.
        /// </summary>
        /// <param name="result">The data returned from the operation.</param>
        /// <returns>
        /// An <see cref="Result{T}"/> object.
        /// </returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>()
            {
                Succeeded = true,
                Value = value
            };
        }

        /// <summary>
        /// Represents that the operation was a failure.
        /// </summary>
        /// <param name="errorType">Represents an error code.</param>
        /// <param name="message">Represents an error message.</param>
        /// <returns>
        /// An <see cref="Result{T}"/> object.
        /// </returns>
        public static Result<T> Failure(ErrorType errorType, string message)
        {
            return new Result<T>()
            {
                Succeeded = false,
                Fault = new ResultFailure()
                {
                    ErrorType = errorType,
                    ErrorMessage = message,
                }
            };
        }
    }
}