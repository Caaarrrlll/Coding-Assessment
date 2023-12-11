using TechSolutionsCRM.DBContexts;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

builder.Services.AddDbContext<IdentityContext>(options => 
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("TechSolutions")
        );
    }
);

builder.Services.AddAuthorization();


builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    swaggerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
