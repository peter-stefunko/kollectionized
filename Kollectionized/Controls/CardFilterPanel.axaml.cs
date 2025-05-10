using Avalonia.Controls;
using Avalonia;

namespace Kollectionized.Controls;

public partial class CardFilterPanel : UserControl
{
    public static readonly StyledProperty<bool> ShowInstanceFiltersProperty =
        AvaloniaProperty.Register<CardFilterPanel, bool>(nameof(ShowInstanceFilters), false);

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