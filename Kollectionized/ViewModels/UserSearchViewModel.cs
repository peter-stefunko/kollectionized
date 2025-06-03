using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class UserSearchViewModel : ViewModelBase
{
    [ObservableProperty] private string _nameQuery = string.Empty;
    [ObservableProperty] private ObservableCollection<UserItemViewModel> _allUsers = [];

    public IEnumerable<UserItemViewModel> FilteredUsers =>
        string.IsNullOrWhiteSpace(NameQuery)
            ? AllUsers
            : AllUsers.Where(u => u.User.Username.Contains(NameQuery, StringComparison.OrdinalIgnoreCase));

    public UserSearchViewModel()
    {
        _ = LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        await RunWithLoading(async () =>
        {
            var users = await UserService.GetAllUsers();

            AllUsers = new ObservableCollection<UserItemViewModel>(
                users
                    .Where(u => !string.IsNullOrWhiteSpace(u.Username))
                    .OrderBy(u => u.Username)
                    .Select(u => new UserItemViewModel(u))
            );

            OnPropertyChanged(nameof(FilteredUsers));
        });
    }
}