using Kollectionized.Models;

namespace Kollectionized.Services;

public static class SessionService
{
    private static User? CurrentUser { get; set; }
    public static bool IsLoggedIn => CurrentUser != null;

    public static void SetUser(User user)
    {
        CurrentUser = user;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}