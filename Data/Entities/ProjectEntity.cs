using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class ProjectEntity
{
    [Key]
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ?Service { get; set; }
    public string ?ProjectManager { get; set; }
    public string ?Status { get; set; }
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
}
