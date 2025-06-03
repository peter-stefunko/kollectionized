using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class UserCardInstanceBrowserViewModel : CardInstanceFilterViewModel
{
    private readonly User _viewedUser;

    public ObservableCollection<CardInstanceItemViewModel> Instances { get; set;  } = new();
    protected List<CardInstanceItemViewModel> AllInstances { get; set; } = new();

    public UserCardInstanceBrowserViewModel(User viewedUser)
    {
        _viewedUser = viewedUser;
    }
    
    public void LoadInstances(List<CardInstance> instances)
    {
        AllInstances.Clear();

        foreach (var instance in instances)
        {
            if (instance.Card != null)
                AllInstances.Add(new CardInstanceItemViewModel(instance.Card, instance));
        }

        ApplyFilters();
    }

    public async Task RefreshInstancesAsync()
    {
        var userInstances = await UserCardService.GetUserCardInstances(_viewedUser.Username);
        LoadInstances(userInstances);
    }

    [RelayCommand]
    protected void ApplyFilters()
    {
        var filtered = AllInstances.Where(vm =>
            (string.IsNullOrWhiteSpace(NameQuery) || vm.Card.Name.Contains(NameQuery, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(SelectedSet) || vm.Card.Set == SelectedSet) &&
            (string.IsNullOrWhiteSpace(SelectedType) || vm.Card.Type.Contains(SelectedType)) &&
            (string.IsNullOrWhiteSpace(SelectedForm) || vm.Card.Form == SelectedForm) &&
            (!SelectedGrade.HasValue || vm.Instance.Grade == SelectedGrade) &&
            (string.IsNullOrWhiteSpace(SelectedCompany) || vm.Instance.GradingCompany == SelectedCompany));

        Instances.Clear();
        foreach (var match in filtered)
            Instances.Add(match);
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();

        foreach (var vm in Instances)
        {
            vm.NotifySessionChanged();
        }
    }
}
