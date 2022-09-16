namespace SonarWave.Core.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the given <paramref name="value"/> from <see langword="string"/>,
        /// to a concrete enum.
        /// </summary>
        /// <typeparam name="TEnum">Represents the enum type.</typeparam>
        /// <param name="value">Represents the value that needs to be parsed.</param>
        /// <returns>
        /// An <see cref="Enum"/>.
        /// </returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            return Enum.TryParse(value, true, out TEnum result) ? result : default;
        }
    }
}