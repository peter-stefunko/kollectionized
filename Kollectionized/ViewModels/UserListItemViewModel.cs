using System;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class UserListItemViewModel : ViewModelBase
{
    public User User { get; }

    public Guid Id => User.Id;
    public string Username => User.Username;

    public IRelayCommand ShowProfileCommand { get; }

    public UserListItemViewModel(User user)
    {
        User = user;
        ShowProfileCommand = new RelayCommand(OpenProfile);
    }

    private void OpenProfile()
    {
        new UserProfileWindow(User).Show();
    }
}