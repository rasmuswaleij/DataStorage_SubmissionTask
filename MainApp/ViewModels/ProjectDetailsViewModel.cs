using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;
public partial class ProjectDetailsViewModel( IServiceProvider serviceProvider) : ObservableObject
{

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private Project _project = new();

    [RelayCommand]
    private void Edit()
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = Project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }
}
