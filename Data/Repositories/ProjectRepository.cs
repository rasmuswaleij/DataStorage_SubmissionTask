using Data.Interfaces;
using Data.Entities;

namespace Business.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\.NetProjects\Graphic_Task\Data\Data\database.mdf;Integrated Security=True;Connect Timeout=30";

    public bool Create(ProjectEntity projectEntity)
    {
        try
        {

        }
        catch
        {

        }
    }

    public IEnumerable<ProjectEntity> GetAll()
    {
        try
        {

        }
        catch
        {

        }
    }
}
