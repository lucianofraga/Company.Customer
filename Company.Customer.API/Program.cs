using Company.Customer.Persistence;
using Company.Customer.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo()
{
    Title = "Customer",
    Description = "Manage Customer's Information",
    Version = "1.0"
}));

builder.Services
    .AddPersistence(builder.Configuration)
    .AddCoreServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseHttpLogging()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();
