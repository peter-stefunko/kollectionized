using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AddCardMenuWindow : Window
{
    public AddCardMenuWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = new AddCardMenuWindowViewModel(card, new UserCardService(), Close);
    }
}