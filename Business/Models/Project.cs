﻿using Data.Entities;

namespace Business.Models;
public class Project
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Service {  get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


}
