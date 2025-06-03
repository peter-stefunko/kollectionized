using System.Text.RegularExpressions;

namespace Kollectionized.Api.Validation;

public static partial class UsernameValidation
{
    private static readonly Regex ValidUsernameRegex =
        AllowedCharsRegex();

    private static readonly HashSet<string> ReservedUsernames = new(StringComparer.OrdinalIgnoreCase)
    {
        "admin", "root", "system", "support", "null", "undefined"
    };

    public static bool IsValid(string username, out string? error)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            error = "Username cannot be empty.";
            return false;
        }

        if (username.Length < 6 || username.Length > 24)
        {
            error = "Username must be between 6 and 24 characters.";
            return false;       
        }
        
        if (!ValidUsernameRegex.IsMatch(username))
        {
            error = "Usernames may only contain letters, digits, underscores, or hyphens.";
            return false;
        }
        
        if (ReservedUsernames.Contains(username))
        {
            error = "That username is reserved.";
            return false;
        }

        error = null;
        return true;
    }

    [GeneratedRegex("^[a-zA-Z0-9_-]+$", RegexOptions.Compiled)]
    private static partial Regex AllowedCharsRegex();
}