using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class CardInstanceDetailsViewModel : CardDetailsViewModel
{
    public CardInstance Instance { get; }

    public string GradeText => Instance.Grade is not null ? $"Grade: {Instance.Grade}" : "Grade: N/A";
    public string CompanyText => !string.IsNullOrWhiteSpace(Instance.GradingCompany) ? $"Company: {Instance.GradingCompany}" : "Company: N/A";
    
    public bool IsCurrentUser => AuthService.CurrentUser?.Id == Instance.CurrentOwner;

    public IRelayCommand ShowDetailsCommand { get; }
    public IRelayCommand EditCommand { get; }
    public IRelayCommand DeleteCommand { get; }

    private readonly Action? _onDeleted;

    public CardInstanceDetailsViewModel(PokemonCard card, CardInstance instance, Action? onDeleted)
        : base(card)
    {
        Instance = instance;
        _onDeleted = onDeleted;

        ShowDetailsCommand = new RelayCommand(OpenDetails);
        EditCommand = new RelayCommand(OpenEditMenu, () => IsCurrentUser);
        DeleteCommand = DeleteInstanceCommand;
    }

    private void OpenDetails()
    {
        new CardInstanceDetailsWindow(Card, Instance).Show();
    }

    private void OpenEditMenu()
    {
        new CardInstanceEditorWindow(Instance, _onDeleted).Show();
    }

    [RelayCommand]
    private async Task DeleteInstanceAsync()
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
            var result = await UserCardService.DeleteCardInstance(Instance.Id);

            if (result != null)
            {
                
                ErrorMessage = result;
                return;
            }
            
            _onDeleted?.Invoke();
        });
    }
    
    [RelayCommand]
    private void AddToCollection()
    {
        if (AuthService.CurrentUser is not { } user) return;

        new AddToCollectionWindow(Instance.Id, user.Username, () => { }).Show();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
        EditCommand.NotifyCanExecuteChanged();
    }
}