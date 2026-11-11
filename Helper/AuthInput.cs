using System.Text.RegularExpressions;

namespace FitAgent.Helper;

public static class AuthInput
{
    public const int MinimumPasswordLength = 4;

    public static string NormalizeEmail(string? email) => (email ?? string.Empty).Trim();

    public static bool EmailsMatch(string? first, string? second) =>
        string.Equals(NormalizeEmail(first), NormalizeEmail(second),
            StringComparison.OrdinalIgnoreCase);

    public static bool IsPasswordValid(string? password) =>
        !string.IsNullOrWhiteSpace(password) && password.Length >= MinimumPasswordLength;

    public static string NormalizePhone(string? phone)
    {
        // Remove formatting only; letters and other symbols remain invalid.
        string cleaned = new((phone ?? string.Empty)
            .Where(c => !char.IsWhiteSpace(c) && c != '-').ToArray());
        if (cleaned.StartsWith("+972", StringComparison.Ordinal))
            cleaned = "0" + cleaned[4..];
        return cleaned;
    }

    public static bool IsPhoneValid(string? phone) =>
        Regex.IsMatch(NormalizePhone(phone), @"^05[0-9]{8}$");
}