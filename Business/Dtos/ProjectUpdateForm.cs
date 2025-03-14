using Data.Entities;

namespace Business.Dtos;
public class ProjectUpdateForm
{
    //Don't if set for Projectnumber should be private or protected.
    //Maybe it should be made in the factory through a helper but I'm not sure
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Service { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
}
