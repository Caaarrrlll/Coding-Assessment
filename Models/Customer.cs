﻿namespace TechSolutionsCRM.Models;

public sealed class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Address>? Addresses { get; set; }
}
