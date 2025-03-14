using Business.Dtos;
using Business.Factories;
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
    private async Task Save()
    {
        var updateForm = ProjectFactory.Back(Project);

        var result =  await _projectService.UpdateProjectAsync((x => x.Id == Project.Id), updateForm);

        if (result != null)
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
