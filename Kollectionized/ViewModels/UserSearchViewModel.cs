using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class UserSearchViewModel : ViewModelBase
{
    private readonly UserService _userService = new();

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private ObservableCollection<UserListItemViewModel> _allUsers = new();

    public IEnumerable<UserListItemViewModel> FilteredUsers =>
        string.IsNullOrWhiteSpace(SearchQuery)
            ? AllUsers
            : AllUsers.Where(u =>
                u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

    public UserSearchViewModel()
    {
        LoadUsers();
        
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(SearchQuery) || e.PropertyName == nameof(AllUsers))
                OnPropertyChanged(nameof(FilteredUsers));
        };
    }

    private async void LoadUsers()
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