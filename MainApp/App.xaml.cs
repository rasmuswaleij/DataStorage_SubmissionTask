using Business.Interfaces;
using Business.Services;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Repositories;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IProjectRepository, ProjectRepository>();
                    services.AddSingleton<ICustomerRepository, CustomerRepository>();

                    services.AddSingleton<IProjectService, ProjectService>();
                    services.AddSingleton<ICustomerService, CustomerService>();


                    services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\.NetProjects\Graphic_Task\Data\Data\database.mdf;Integrated Security=True;Connect Timeout=30"), ServiceLifetime.Scoped);
                    services.AddDbContextFactory<DataContext>(options =>
    options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\.NetProjects\Graphic_Task\Data\Data\database.mdf;Integrated Security=True;Connect Timeout=30"));

                    services.AddTransient<ProjectListViewModel>();
                    services.AddTransient<ProjectAddViewModel>();
                    services.AddTransient<ProjectDetailsViewModel>();
                    services.AddTransient<ProjectEditViewModel>();

                    services.AddTransient<ProjectListView>();
                    services.AddTransient<ProjectAddView>();
                    services.AddTransient<ProjectDetailsView>();
                    services.AddTransient<ProjectEditView>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ProjectListViewModel>();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

        }

    }

}
