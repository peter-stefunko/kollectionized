using Avalonia.Controls;
using Avalonia.Input;
using Kollectionized.ViewModels;

namespace Kollectionized.Controls;

public partial class CardItemControl : UserControl
{
    public CardItemControl()
    {
        InitializeComponent();
    }

    private void OnCardClick(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is CardItemViewModel vm)
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}