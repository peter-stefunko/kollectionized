using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Utils;

namespace Kollectionized.ViewModels;

public partial class CardInstanceEditViewModel : MenuWindowBase
{
    private readonly CardInstance _instance;

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    public CardInstanceEditViewModel(CardInstance instance)
    {
        _instance = instance;
        SelectedGrade = instance.Grade;
        SelectedGradingCompany = instance.GradingCompany;
        Notes = instance.Notes;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await RunWithLoading(async () =>
        {
            var result = await UserCardService.UpdateCardInstance(
                _instance.Id, SelectedGrade, SelectedGradingCompany, Notes);

            if (result != null) ErrorMessage = result;
            else AppNavigation.GoBack();
        });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        var confirm = await MsBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandard(new MsBox.Avalonia.Dto.MessageBoxStandardParams
            {
                ContentTitle = "Delete instance?",
                ContentMessage = "Are you sure?",
                ButtonDefinitions = MsBox.Avalonia.Enums.ButtonEnum.YesNo
            }).ShowAsync();

        if (confirm != MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            return;
        }

        await RunWithLoading(async () =>
        {
            var result = await UserCardService.DeleteCardInstance(_instance.Id);

            if (result != null)
            {
                ErrorMessage = result;
                return;
            }

            AppNavigation.GoBack();
        });
    }
}