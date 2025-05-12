using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class AddCardMenuWindowViewModel(PokemonCard card, Action onClose) : MenuWindowBase(onClose)
{
    [RelayCommand]
    private async Task AddAsync()
    {
        if (!ValidateInputs()) return;

        await RunWithLoading(async () =>
        {
            var error = await UserCardService.AddCardInstance(card.Uuid, SelectedGrade, SelectedGradingCompany,
                Notes);
            
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }
            
            OnClose?.Invoke();
        });
    }
}