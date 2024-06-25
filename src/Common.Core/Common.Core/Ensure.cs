using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Common.Core
{
    public static class Ensure
    {
        public static void IsNotNullOrEmpty(
            [NotNull] string? value,
            [CallerArgumentExpression("value")] string? paramName = default)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsNotDefault<T>(
            T value,
            [CallerArgumentExpression("value")] string? paramName = default) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                throw new ArgumentException($"The parameter '{paramName}' should not be the default value.", paramName);
            }
        }
    }
}
