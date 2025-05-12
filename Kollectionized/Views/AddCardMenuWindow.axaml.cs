using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AddCardMenuWindow : WindowBase
{
    public AddCardMenuWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = new AddCardMenuWindowViewModel(card, Close);
    }
}