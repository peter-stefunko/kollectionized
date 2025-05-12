using Avalonia.Controls;
using Avalonia;

namespace Kollectionized.Controls;

public partial class CardFilterPanel : UserControl
{
    private static readonly StyledProperty<bool> ShowInstanceFiltersProperty =
        AvaloniaProperty.Register<CardFilterPanel, bool>(nameof(ShowInstanceFilters));

    public bool ShowInstanceFilters
    {
        get => GetValue(ShowInstanceFiltersProperty);
        set => SetValue(ShowInstanceFiltersProperty, value);
    }

    public CardFilterPanel()
    {
        InitializeComponent();
    }
}