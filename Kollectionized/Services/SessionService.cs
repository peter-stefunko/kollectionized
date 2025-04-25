using Kollectionized.Models;

namespace Kollectionized.Services;

public static class SessionService
{
    public static User? CurrentUser { get; private set; }

    public static void SetUser(User user)
    {
        CurrentUser = user;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }

    public static bool IsLoggedIn => CurrentUser != null;
}