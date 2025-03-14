using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;
public interface IProjectRepository
{

    //bool Create(ProjectEntity projectEntity);
    //IEnumerable<ProjectEntity> GetAll();

    Task<ProjectEntity> CreateAsync(ProjectEntity entity);
    Task<IEnumerable<ProjectEntity>> GetAllAsync();
    Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity);
    Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression);
}
