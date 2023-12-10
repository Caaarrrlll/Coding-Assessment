using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechSolutionsCRM.Server.Models;

namespace TechSolutionsCRM.DBContexts;

public class TechSolutionsCRMContext: DbContext
{
    public TechSolutionsCRMContext(DbContextOptions<TechSolutionsCRMContext> options) : base(options) { }

    #region Properties
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Address> Addresses { get; set; }
    #endregion

    #region Context Config
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("CRM");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    #endregion
}
