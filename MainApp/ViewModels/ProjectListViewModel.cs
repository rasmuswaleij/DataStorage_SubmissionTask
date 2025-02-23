using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;

namespace MainApp.ViewModels;
public partial class ProjectListViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly IServiceProvider _serviceProvider;

    public ProjectListViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;

        var project1 = new Project()
        {
            ProjectManager = "Rasmus Waleij",
            ProjectName = "Programutveckling",
            Service = "Konsult",
            Status = "Ongoing"
        };

        var enumerable = new[] { project1 };

        _projects = new ObservableCollection<Project>(enumerable);

        //_projects = new ObservableCollection<Project>(_projectService.GetAll);
    }

    [ObservableProperty]
    private ObservableCollection<Project> _projects = [];

    [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
    }

    [RelayCommand]
    private void GoToDetailsView(Project project)
    {
        var projectDetailsViewModel = _serviceProvider.GetRequiredService<ProjectDetailsViewModel>();
        projectDetailsViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectDetailsViewModel;
    }
}
