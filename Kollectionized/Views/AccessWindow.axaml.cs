using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AccessWindow : WindowBase
{
    public AccessWindow()
    {
        InitializeComponent();
        DataContext = new AccessWindowViewModel(Close);
    }
}