using System;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized;

public static class ViewModelLocator
{
    private static readonly CardService CardService = new();
    private static readonly CardImageService CardImageService = new();
    private static readonly UserService UserService = new();
    
    public static CardGamesViewModel CardGames => new();

    public static MainWindowViewModel MainWindow => new();

    public static CardGridBrowserViewModel CreateCardGridBrowser(string gameKey) =>
        new(gameKey, CardService, CardImageService);
    
    /*public static CardItemViewModel CreateCardItemViewModel(PokemonCard card) =>
        new(card, CardImageService);*/

    public static CardDetailsViewModel CreateCardDetailsViewModel(PokemonCard card) =>
        new(card, CardImageService);

    /*public static UserProfileViewModel CreateUserProfile(User user, Action? onDeleteSuccess = null) =>
        new(user, UserService, onDeleteSuccess);*/

    public static UserSearchViewModel UserSearch => new(UserService);

    public static LoginViewModel CreateLoginViewModel(Action? switchToRegister = null, Action? onLoginSuccess = null) =>
        new(UserService, switchToRegister, onLoginSuccess);

    public static RegisterViewModel CreateRegisterViewModel(Action? switchToLogin = null, Action? onRegisterSuccess = null) =>
        new(UserService, switchToLogin, onRegisterSuccess);

    public static AccessWindowViewModel CreateAccessWindowViewModel(Action? closeWindow) =>
        new(
            createLoginViewModel: onLogin => CreateLoginViewModel(onLogin, closeWindow),
            createRegisterViewModel: onRegister => CreateRegisterViewModel(onRegister, closeWindow)
        );
}