using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Kollectionized.Converters;

public class NullableGradeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is double d ? d.ToString("0.0") : "";

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => null;
}