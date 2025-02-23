using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;
public partial class ProjectEditViewModel(IProjectService projectService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IProjectService _projectService = projectService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private Project _project = new ();

    [RelayCommand]
    private void Save()
    {
        //var result = _projectService.UpdateProject(Project);

        var result = true;

        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }
}
