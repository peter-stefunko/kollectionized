using System;

namespace Kollectionized.Services;

public static class AuthService
{
    public static Guid? CurrentUserId { get; private set; }

    public static string? CurrentUsername { get; private set; }

    public static bool IsLoggedIn => CurrentUserId != null;
    public static event Action? SessionChanged;

    public static void Login(Guid userId, string username)
    {
        CurrentUserId = userId;
        CurrentUsername = username;
        SessionChanged?.Invoke();
    }

    public static void Logout()
    {
        CurrentUserId = null;
        CurrentUsername = null;
        SessionChanged?.Invoke();
    }
}