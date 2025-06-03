using Avalonia.Controls;
using Avalonia.Input;
using Kollectionized.ViewModels;

namespace Kollectionized.Controls;

public partial class CardItemView : UserControl
{
    public CardItemView()
    {
        InitializeComponent();
    }

    private void OnCardClicked(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is CardItemViewModel vm)
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}