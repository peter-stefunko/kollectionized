using Kollectionized.State;
using System.Threading.Tasks;

namespace Kollectionized.Services;

public static class AuthService
{
    private static readonly UserService UserService = new();

    public static async Task<bool> TryLogin(string username, string password)
    {
        var user = await UserService.Login(username, password);
        if (user == null) return false;

        CurrentUserState.Login(user, password);
        return true;
    }

    public static void Logout()
    {
        CurrentUserState.Logout();
    }
}