namespace Business.Dtos;
public class ProjectRegistrationForm
{
    //-- Everything except ID --
    public string ProjectName { get; set; } = null!;
    public string Service { get; set; } = null!;

    public string ProjectManager { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string CustomerName { get; set; } = null!;
}
