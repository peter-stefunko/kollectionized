using System;
using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CreateDeckWindow : Window
{
    public CreateDeckWindow(string username, Action onClose)
    {
        InitializeComponent();
        DataContext = new CreateDeckWindowViewModel(username, onClose);
    }
}