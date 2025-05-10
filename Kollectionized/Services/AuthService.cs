using System;
using Kollectionized.Models;

namespace Kollectionized.Services;

public static class AuthService
{
    public static User? CurrentUser { get; private set; }
    public static string? CurrentPassword { get; private set; }

    public static bool IsLoggedIn => CurrentUser != null;
    public static event Action? SessionChanged;

    public static void Login(User user, string password)
    {
        CurrentUser = user;
        CurrentPassword = password;
        SessionChanged?.Invoke();
    }

    public static void Logout()
    {
        CurrentUser = null;
        CurrentPassword = null;
        SessionChanged?.Invoke();
    }
}