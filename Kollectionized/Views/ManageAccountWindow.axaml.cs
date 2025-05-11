using Avalonia.Controls;
using Kollectionized.ViewModels;
using Kollectionized.Services;

namespace Kollectionized.Views;

public partial class ManageAccountWindow : Window
{
    public ManageAccountWindow(UserProfileViewModel profileViewModel)
    {
        InitializeComponent();
        DataContext = new ManageAccountViewModel(profileViewModel, new UserService(), Close);
    }
}