using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class UserSearchViewModel : ViewModelBase
{
    [ObservableProperty] private string _searchQuery = string.Empty;
    [ObservableProperty] private ObservableCollection<UserListItemViewModel> _allUsers = [];

    public IEnumerable<UserListItemViewModel> FilteredUsers =>
        string.IsNullOrWhiteSpace(SearchQuery)
            ? AllUsers
            : AllUsers.Where(u => u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

    public UserSearchViewModel()
    {
        _ = RunWithLoading(LoadUsersAsync);
    }

    private async Task LoadUsersAsync()
    {
        var users = await UserService.GetAllUsers();

        AllUsers = new ObservableCollection<UserListItemViewModel>(
            users
                .Where(u => !string.IsNullOrWhiteSpace(u.Username))
                .OrderBy(u => u.Username)
                .Select(u => new UserListItemViewModel(u))
        );

        OnPropertyChanged(nameof(FilteredUsers));
    }
}