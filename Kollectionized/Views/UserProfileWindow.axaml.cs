using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized;
using Kollectionized.Models;

namespace Kollectionized.Views;

public partial class UserProfileWindow : Window
{
    public UserProfileWindow(User user)
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateUserProfile(user, Close);
    }
}