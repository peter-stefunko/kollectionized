using System;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Views;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class UserListItemViewModel
{
    public Guid Id { get; }
    public string Username { get; }

    public IRelayCommand ShowProfileCommand { get; }

    public UserListItemViewModel(User user)
    {
        Id = user.Id;
        Username = user.Username;

        ShowProfileCommand = new RelayCommand(() =>
        {
            new UserProfileWindow(Id, Username).Show();
        });
    }
}