﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;

namespace MainApp.ViewModels;
public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        //CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

}
