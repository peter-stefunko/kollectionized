using Avalonia.Controls;
using Kollectionized.ViewModels;
using Kollectionized.Services;

namespace Kollectionized.Views;

public partial class ManageAccountWindow : WindowBase
{
    public ManageAccountWindow(UserProfileViewModel profileViewModel)
    {
        InitializeComponent();
        DataContext = new ManageAccountViewModel(profileViewModel, Close);
    }
}