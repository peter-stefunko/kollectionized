using Avalonia.Controls;
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