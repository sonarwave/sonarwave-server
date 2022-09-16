namespace SonarWave.Application.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            return Enum.TryParse(value, true, out TEnum result) ? result : default;
        }
    }
}