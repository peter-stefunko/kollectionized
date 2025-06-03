using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Kollectionized.Views;

namespace Kollectionized.Utils;

public static class AppNavigation
{
    public static MainWindow? GetMainWindow()
    {
        return (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow;
    }

    public static void NavigateTo(UserControl view)
    {
        GetMainWindow()?.NavigateTo(view);
    }

    public static void GoBack()
    {
        GetMainWindow()?.GoBack();
    }
}