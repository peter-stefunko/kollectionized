using System;
using Avalonia.Controls;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class WindowBase : Window
{
    public WindowBase()
    {
        AuthService.SessionChanged += OnSessionChanged;
    }

    protected void OnSessionChanged()
    {
        if (DataContext is ViewModelBase vm)
        {
            vm.NotifySessionChanged();
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        AuthService.SessionChanged -= OnSessionChanged;
    }
}