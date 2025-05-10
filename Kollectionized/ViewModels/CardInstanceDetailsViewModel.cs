using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public partial class CardInstanceDetailsViewModel : CardDetailsViewModel
{
    public CardInstance Instance { get; }

    public string GradeText => Instance.Grade is not null ? $"Grade: {Instance.Grade}" : "Grade: N/A";
    public string CompanyText => !string.IsNullOrWhiteSpace(Instance.GradingCompany) ? $"Company: {Instance.GradingCompany}" : "Company: N/A";
    public string Notes => string.IsNullOrWhiteSpace(Instance.Notes) ? "No notes." : Instance.Notes;

    public Guid InstanceId => Instance.Id;
    public bool IsCurrentUser => AuthService.CurrentUser?.Id == Instance.CurrentOwner;

    public IRelayCommand ShowDetailsCommand { get; }
    public IRelayCommand EditCommand { get; }
    public IRelayCommand DeleteCommand { get; }

    public CardInstanceDetailsViewModel(PokemonCard card, CardInstance instance, CardImageService imageService)
        : base(card, imageService)
    {
        Instance = instance;

        ShowDetailsCommand = new RelayCommand(OpenDetails);
        EditCommand = new RelayCommand(OpenEditMenu, () => IsCurrentUser);
        DeleteCommand = new RelayCommand(DeleteInstanceAsync, () => IsCurrentUser);
    }

    private void OpenDetails()
    {
        new CardInstanceDetailsWindow(Card, Instance).Show();
    }

    private void OpenEditMenu()
    {
        new CardInstanceEditorWindow(Instance).Show();
    }

    private async void DeleteInstanceAsync()
    {
        var confirmed = await DialogService.ConfirmAsync(
            "Are you sure you want to delete this card instance?",
            "Confirm Deletion");

        if (!confirmed) return;

        var user = AuthService.CurrentUser;
        var password = AuthService.CurrentPassword;

        if (user == null || string.IsNullOrWhiteSpace(password))
        {
            /*await DialogService.AlertAsync("User is not authenticated.", "Error");*/
            return;
        }

        var result = await new UserCardService().DeleteCardInstance(Instance.Id);

        /*if (result != null)
            await DialogService.AlertAsync(result, "Error");
        else
            await DialogService.AlertAsync("Card instance deleted.", "Success");*/

        // Optional: trigger parent VM refresh here
    }
}
