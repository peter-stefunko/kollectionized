using System;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Kollectionized.ViewModels;

namespace Kollectionized;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? param)
    {
        if (param is null)
            return new TextBlock { Text = "Null parameter passed to ViewLocator." };

        var viewModelType = param.GetType();
        var viewTypeName = viewModelType.FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);

        // Try to load the view type directly
        // Fallback: look through the current assembly manually
        var viewType = Type.GetType(viewTypeName) ?? Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.FullName == viewTypeName);

        if (viewType == null || !typeof(Control).IsAssignableFrom(viewType))
            return new TextBlock { Text = $"View not found for: {viewModelType.FullName}" };

        var control = (Control)Activator.CreateInstance(viewType)!;
        control.DataContext = param;
        return control;

    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}