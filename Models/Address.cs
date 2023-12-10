namespace TechSolutionsCRM.Models;

public sealed class Address
{
    public int? Id { get; set; }
    public string? AddressName { get; set; }
    public string? AddressType { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Province { get; set; }
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
}
