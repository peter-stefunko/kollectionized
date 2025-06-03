using Avalonia.Controls;
using Avalonia.Input;
using Kollectionized.ViewModels;

namespace Kollectionized.Controls;

public partial class CardInstanceItemControl : UserControl
{
    public CardInstanceItemControl()
    {
        InitializeComponent();
    }

    private void OnCardClick(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is CardInstanceItemViewModel vm)
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}