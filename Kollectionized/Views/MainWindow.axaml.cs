using Avalonia.Controls;
using System.Collections.Generic;

namespace Kollectionized.Views;

public partial class MainWindow : Window
{
    private readonly Stack<UserControl> _viewStack = new();

    public MainWindow()
    {
        InitializeComponent();
        NavigateTo(new HomeView());
    }

    public void NavigateTo(UserControl view)
    {
        _viewStack.Push(view);
        ViewHost.Content = view;
    }

    public void GoBack()
    {
        if (_viewStack.Count <= 1)
            return;
        
        _viewStack.Pop();
        ViewHost.Content = _viewStack.Peek();
    }
}