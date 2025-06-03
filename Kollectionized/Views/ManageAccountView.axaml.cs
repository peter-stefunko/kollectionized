using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class ManageAccountView : UserControl
{
    public ManageAccountView(UserProfileViewModel profile)
    {
        InitializeComponent();
        DataContext = new ManageAccountViewModel(profile);
    }
}