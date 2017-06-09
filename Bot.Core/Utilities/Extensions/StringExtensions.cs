using System;

namespace Bot.Core.Utilities.Extensions
{/// <summary>
 /// <see cref="string"/> extensions.
 /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Performs a "human-like" comparison of <paramref name="value"/> and <paramref name="comparison"/>.
        /// (e.g. null = empty, trimmed, case insensitive, using current culture).
        /// </summary>
        public static bool HumanEquals(this string value, string comparison)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.IsNullOrEmpty(comparison);
            }

            return value.Trim().Equals(comparison, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
