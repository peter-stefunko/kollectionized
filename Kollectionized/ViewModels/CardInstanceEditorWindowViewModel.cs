using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class CardInstanceEditorWindowViewModel : MenuWindowBase
{
    private readonly UserCardService _userCardService;
    private readonly CardInstance _instance;

    public Guid InstanceId => _instance.Id;
    public bool IsCurrentUser => AuthService.CurrentUser?.Id == _instance.CurrentOwner;

    public CardInstanceEditorWindowViewModel(CardInstance instance, UserCardService userCardService, Action onClose)
        : base(onClose)
    {
        _instance = instance;
        _userCardService = userCardService;

        SelectedGrade = _instance.Grade;
        SelectedGradingCompany = _instance.GradingCompany;
        Notes = _instance.Notes;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidateInputs()) return;

        var user = AuthService.CurrentUser;
        var password = AuthService.CurrentPassword;

        if (user == null || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessage = "User is not authenticated.";
            return;
        }

        await RunWithLoading(async () =>
        {
            var result = await _userCardService.UpdateCardInstance(
                _instance.Id, SelectedGrade, SelectedGradingCompany, Notes);

            if (result != null)
            {
                ErrorMessage = result;
                return;
            }

            _onClose?.Invoke();
        });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        var msgBox = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ButtonDefinitions = ButtonEnum.YesNoCancel,
            ContentTitle = "Confirm",
            ContentMessage = "Are you sure you want to delete this instance?",
            Icon = Icon.Warning,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        });

        var confirmed = await msgBox.ShowAsync();
        if (confirmed != ButtonResult.Yes)
            return;

        await RunWithLoading(async () =>
        {
            var result = await _userCardService.DeleteCardInstance(_instance.Id);

            if (result != null)
            {
                ErrorMessage = result;
                return;
            }

            _onClose?.Invoke();
        });
    }
}