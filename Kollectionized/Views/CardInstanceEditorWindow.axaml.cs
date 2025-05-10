using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceEditorWindow : Window
{
    public CardInstanceEditorWindow(CardInstance instance)
    {
        InitializeComponent();
        DataContext = new CardInstanceEditorWindowViewModel(instance, new UserCardService(), Close);
    }
}