using Kollectionized.Models;
using System;

namespace Kollectionized.State;

public static class CurrentUserState
{
    public static User? User { get; private set; }
    public static string? Password { get; private set; }

    public static bool IsLoggedIn => User != null;

    public static event Action? SessionChanged;

    public static void Login(User user, string password)
    {
        User = user;
        Password = password;
        SessionChanged?.Invoke();
    }

    public static void Logout()
    {
        User = null;
        Password = null;
        SessionChanged?.Invoke();
    }
}