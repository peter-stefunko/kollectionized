using System;
using Avalonia.Data.Converters;
using System.Globalization;
using Avalonia;

namespace Kollectionized.Converters;

public class BooleanNegationConverter : IValueConverter
{
    public static readonly BooleanNegationConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : AvaloniaProperty.UnsetValue;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : AvaloniaProperty.UnsetValue;
}