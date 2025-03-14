using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Collections.ObjectModel;

namespace Business.Factories;
public static class ProjectFactory
{




    //public static async ObservableCollection<Project> Wait(Task<IEnumerable<Project>>)
    //{
    //    await Task.Run(() => {
    //}

    public static ProjectRegistrationForm Create() => new();
    public static ProjectEntity? Create(ProjectRegistrationForm form, CustomerEntity customerEntity) => form == null ? null : new()
    {
        Id = Guid.NewGuid(),
        ProjectName = form.ProjectName,
        ProjectManager = form.ProjectManager,
        Service = form.Service,
        Status = form.Status,
        Customer = customerEntity
    };

    public static Project? Create(ProjectEntity entity) => entity == null ? null : new()
    {
        //-- ID is supposed to be here as well --
        Id = entity.Id,
        ProjectName = entity.ProjectName,
        ProjectManager = entity.ProjectManager,
        Service = entity.Service,
        Status = entity.Status,
        Customer = entity.Customer

    };

    public static ProjectUpdateForm Create(Project project) => new()
    {
        ProjectName = project.ProjectName,
        ProjectManager = project.ProjectManager,
    };

    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        ProjectName = form.ProjectName,
        ProjectManager = form.ProjectManager,
        Service = form.Service,
        Status = form.Status,
        Customer = form.Customer
    };

    public static ProjectUpdateForm Back(Project project) => new()
    {
        Id = project.Id,
        ProjectName = project.ProjectName,
        ProjectManager = project.ProjectManager,
        Service = project.Service,
        Status = project.Status,
        CustomerId = project.Customer.Id,
        Customer = project.Customer
    };
}
