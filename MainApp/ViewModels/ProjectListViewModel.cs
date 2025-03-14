using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MainApp.ViewModels;
public partial class ProjectListViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly IServiceProvider _serviceProvider;


    [ObservableProperty]
    private ObservableCollection<Project> _projects = [];

    public ProjectListViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;

        //Idén kring utförandet av metoden Load() är delvis hämtad från ChatGPT
        _ = Load();
    }

    public async Task Load()
    {
        try
        {
            var projects = await _projectService.GetAllProjectsAsync();
            if (projects != null)
            {
                foreach (var project in projects)
                {
                    Debug.WriteLine("A project is added to the observable collection");
                    Projects.Add(project);
                }
            }
        }
        finally
        {
            Debug.WriteLine("LoadProjectsAsync är klar.");
        }
    }


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
