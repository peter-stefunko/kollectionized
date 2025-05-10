using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class AddCardMenuWindowViewModel : MenuWindowBase
{
    private readonly UserCardService _userCardService;
    private readonly PokemonCard _card;

    public AddCardMenuWindowViewModel(PokemonCard card, UserCardService userCardService, Action onClose)
        : base(onClose)
    {
        _card = card;
        _userCardService = userCardService;
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        if (!ValidateInputs()) return;

        var userId = AuthService.CurrentUser?.Id ?? throw new InvalidOperationException("User must be logged in.");

        await RunWithLoading(async () =>
        {
            /*var error = await _userCardService.AddCardInstance(new CardInstanceCreateRequest
                {
                    CardId = _card.Id,
                    CurrentOwner = userId,
                    Grade = SelectedGrade,
                    GradingCompany = SelectedGradingCompany,
                    Notes = Notes
                });*/

            var error = await _userCardService.AddCardInstance(_card.Uuid, SelectedGrade, SelectedGradingCompany,
                Notes);
            
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }
            
            _onClose?.Invoke();
        });
    }
}