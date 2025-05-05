using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserProfileWindow : Window
{
    public UserProfileWindow(Guid viewedUserId, string viewedUsername)
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel(viewedUserId, viewedUsername, Close);
    }
}
