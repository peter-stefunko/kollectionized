using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    public User User { get; }

    public Guid ViewedUserId => User.Id;
    public string ViewedUsername => User.Username;
    public string Bio => User.Bio;
    public DateTime CreatedAt => User.CreatedAt;

    public bool IsCurrentUser => User.Id == AuthService.CurrentUser?.Id;

    public CardInstanceGridBrowserViewModel CardGridViewModel { get; }

    public UserProfileViewModel(User user)
    {
        User = user;
        CardGridViewModel = new CardInstanceGridBrowserViewModel(user);
    }
}