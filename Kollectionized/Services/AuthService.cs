using System;

namespace Kollectionized.Services;

public static class AuthService
{
    private static Guid? _currentUserId;
    private static string? _username;

    public static Guid? CurrentUserId => _currentUserId;
    public static string? CurrentUsername => _username;
    public static bool IsLoggedIn => _currentUserId != null;

    public static void Login(Guid userId, string username)
    {
        _currentUserId = userId;
        _username = username;
    }

    public static void Logout()
    {
        _currentUserId = null;
        _username = null;
    }

    public static void RequireLogin()
    {
        if (!IsLoggedIn)
            throw new InvalidOperationException("User must be logged in.");
    }
}