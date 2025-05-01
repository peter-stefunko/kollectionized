using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Kollectionized.ViewModels;

namespace Kollectionized;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return new TextBlock { Text = "Null parameter passed to ViewLocator." };

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = param;
            return control;
        }

        return new TextBlock { Text = $"View not found for: {param.GetType().FullName}" };
    }
    
    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}