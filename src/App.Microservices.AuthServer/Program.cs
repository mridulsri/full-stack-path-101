using FluentValidation.AspNetCore;
using App.Infrastructure.Logging;
using App.Microservices.AuthServer.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Host.UseAppLogger(configuration);

builder.Services.AddServiceFramework(configuration);
builder.Services.AddDataStore<AuthDbContext>(configuration);

builder.Services.AddAuthModules(configuration);

builder.Services.AddControllers().AddFluentValidation(x => x.AutomaticValidationEnabled = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseServiceFramework();
app.UseDataStoreMigration<AuthDbContext>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
