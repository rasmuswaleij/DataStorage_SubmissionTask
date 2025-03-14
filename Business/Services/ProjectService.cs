using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;
public class ProjectService(IProjectRepository projectRepository, ICustomerService customerService) : IProjectService
{

    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerService _customerService = customerService;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var customerEntity = await _customerService.CreateCustomerAsync(form.CustomerName);

        var entity = await _projectRepository.GetAsync(x => x.ProjectName == form.ProjectName);
        entity ??= await _projectRepository.CreateAsync(ProjectFactory.Create(form, customerEntity) ?? null!);

        return ProjectFactory.Create(entity) ?? null!;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        Debug.WriteLine("GetAllProjectsAsync anropas");
        var entities = await _projectRepository.GetAllAsync() ?? [];
        var projects = entities.Select(ProjectFactory.Create);

        return projects;
    }

    public async Task<Project> GetProjectByExpressionAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }

    public async Task<Project> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectUpdateForm form)
    {

        var entity = await _projectRepository.UpdateAsync(expression, ProjectFactory.Create(form));
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }




    public async Task<bool> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var result = await _projectRepository.DeleteAsync(expression);
        return result;
    }
}

