using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;
public partial class ProjectAddViewModel(IProjectService projectService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IProjectService _projectService = projectService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;


    [ObservableProperty]
    private ProjectRegistrationForm _project = new(); 

    [RelayCommand]
    private async Task Save()
    {
        var result = await _projectService.CreateProjectAsync(Project);

        if (result != null)
        {
            //var projectListViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
            //await projectListViewModel.LoadProjectsAsync();

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();

            //var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
            //projectAddViewModel.Project = Project;

            //var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            //mainViewModel.CurrentViewModel = projectAddViewModel;
        }
    }

    [RelayCommand]
    private void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

   

}
