using TechSolutionsCRM.DBContexts;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

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

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICustomerService, CustomerService>();

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
