using Business.Dtos;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;
public partial class ProjectAddViewModel(IProjectService projectService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IProjectService _projectService = projectService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    //-- MÅSTE SKAPA HELA BACKEND INNAN JAG BYTER UT TILL DET JAG SKRIVIT INOM KOMMENTARERNA - BYGGT FÖR ATT
    //SERVICE.CREATEPROJECT SKA RETURNERA ETT BOOL-VÄRDE

    [ObservableProperty]
    private ProjectRegistrationForm _project = new(); 

    [RelayCommand]
    private void Save()
    {
        //var result = _projectService.CreateProject(Project);

        var result = true;

        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
        }
    }

    [RelayCommand]
    private void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

   

}
