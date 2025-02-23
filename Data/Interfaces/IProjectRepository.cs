using Data.Entities;

namespace Data.Interfaces;
public interface IProjectRepository
{
    bool Create(ProjectEntity projectEntity);
    IEnumerable<ProjectEntity> GetAll();
}
