using Avalonia.Controls;
using Avalonia.Input;
using Kollectionized.ViewModels;

namespace Kollectionized.Controls;

public partial class CardInstanceItemView : UserControl
{
    public CardInstanceItemView()
    {
        InitializeComponent();
    }

    private void OnClick(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is CardInstanceItemViewModel vm)
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}