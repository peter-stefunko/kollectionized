using System;
using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AddToCollectionWindow : Window
{
    public AddToCollectionWindow(Guid instanceId, string username, Action onClose)
    {
        InitializeComponent();
        DataContext = new AddToCollectionWindowViewModel(instanceId, username, onClose);
    }
}