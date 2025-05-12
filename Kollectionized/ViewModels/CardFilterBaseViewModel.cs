using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Common;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public abstract partial class CardFilterBaseViewModel : ViewModelBase
{
    [ObservableProperty] private string _nameQuery = string.Empty;

    [ObservableProperty] private ObservableCollection<string> _types = new(Constants.PokemonTypes);
    [ObservableProperty] private string? _selectedType;

    [ObservableProperty] private ObservableCollection<string> _typings = new(Constants.PokemonTypings);
    [ObservableProperty] private string? _selectedTyping;

    [ObservableProperty] private ObservableCollection<string> _forms = new(Constants.PokemonForms);
    [ObservableProperty] private string? _selectedForm;

    [ObservableProperty] private ObservableCollection<string> _sets = new(Constants.PokemonSets);
    [ObservableProperty] private string? _selectedSet;
    
    [ObservableProperty] private PokemonSet? _setInfo;

    partial void OnSelectedSetChanged(string? value)
    {
        _ = LoadSetInfoAsync(value);
    }

    private async Task LoadSetInfoAsync(string? setName)
    {
        SetInfo = !string.IsNullOrWhiteSpace(setName)
            ? await SetsService.GetSetByNameAsync(setName)
            : null;
    }
}