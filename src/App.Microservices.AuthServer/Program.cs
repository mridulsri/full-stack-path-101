using App.Microservices.AuthServer.Configs;
using App.Microservices.AuthServer.Services;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

var appSettings = new AppSettings();
configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddServiceFramework(configuration);
builder.Services.AddDataStore(configuration);

builder.Services.AddAuthModules(configuration);

builder.Services.AddControllers().AddFluentValidation(x => x.AutomaticValidationEnabled = false); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseServiceFramework();
app.UseDataStore();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
