using System;
using Kollectionized.Models;

namespace Kollectionized.Services;

public static class AuthService
{
    public static User? CurrentUser { get; private set; }

    public static bool IsLoggedIn => CurrentUser != null;

    public static event Action? SessionChanged;

    public static void Login(User user)
    {
        CurrentUser = user;
        SessionChanged?.Invoke();
    }

    public static void Logout()
    {
        CurrentUser = null;
        SessionChanged?.Invoke();
    }
}