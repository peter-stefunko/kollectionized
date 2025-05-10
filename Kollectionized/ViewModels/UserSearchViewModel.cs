using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class UserSearchViewModel : ViewModelBase
{
    private readonly UserService _userService;

    [ObservableProperty] private string _searchQuery = string.Empty;
    [ObservableProperty] private ObservableCollection<UserListItemViewModel> _allUsers = new();

    public IEnumerable<UserListItemViewModel> FilteredUsers =>
        string.IsNullOrWhiteSpace(SearchQuery)
            ? AllUsers
            : AllUsers.Where(u => u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

    public UserSearchViewModel(UserService userService)
    {
        _userService = userService;
        _ = RunWithLoading(LoadUsersAsync);
    }

    private async Task LoadUsersAsync()
    {
        var users = await _userService.GetAllUsers();

        AllUsers = new ObservableCollection<UserListItemViewModel>(
            users
                .Where(u => !string.IsNullOrWhiteSpace(u.Username))
                .OrderBy(u => u.Username)
                .Select(u => new UserListItemViewModel(u))
        );

        OnPropertyChanged(nameof(FilteredUsers));
    }
}