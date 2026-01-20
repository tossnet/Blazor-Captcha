using System.Security.Cryptography;

namespace BlazorCaptcha.Commun;

/// <summary>
/// Utility class for CAPTCHA generation.
/// </summary>
public static class Tools
{
    /// <summary>
    /// Characters available for CAPTCHA generation.
    /// Excluded: I, O, S, V, X, l, o, s, v, x, 0, 1 (to avoid visual confusion).
    /// </summary>
    private const string Chars = "ABCDEFGHJKLMNPQRTUWYZabcdefghijkmnpqrtuwz23456789*#!$%=@?";

    /// <summary>
    /// Generates a cryptographically secure random word for the CAPTCHA.
    /// </summary>
    /// <param name="length">The length of the word to generate.</param>
    /// <returns>A random string of the specified length.</returns>
    /// <remarks>
    /// Uses <see cref="RandomNumberGenerator"/> for secure generation
    /// and <see cref="string.Create"/> for optimal memory allocation (zero intermediate allocations).
    /// </remarks>
    public static string GetCaptchaWord(int length)
    {
        // string.Create directly allocates the final string and provides a Span<char> for writing
        // The 'static' keyword on the lambda prevents variable capture (better performance)
        return string.Create(length, Chars, static (span, chars) =>
        {
            for (int i = 0; i < span.Length; i++)
            {
                // GetInt32 generates a cryptographically secure random integer
                // between 0 (inclusive) and chars.Length (exclusive)
                span[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
            }
        });
    }
}
