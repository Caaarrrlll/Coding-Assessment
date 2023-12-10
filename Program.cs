using TechSolutionsCRM.DBContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<TechSolutionsCRMContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("TechSolutions"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("TechSolutionsCRM");
                sqlOptions.MigrationsHistoryTable("__EFMigrationHistory", "CRM");
            }
       );
    }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
