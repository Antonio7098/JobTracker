using JobTracker.Api.Models;
using JobTracker.Api.Services;
using JobTracker.Api.Endpoints;
using JobTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployersRepository, MySqlEmployersRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

builder.Services.AddDbContext<JobTrackerDbContext>(options =>
{

    options.UseMySql(connectionString, serverVersion);

}, ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var XmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, XmlFilename);

    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.MapEmployersEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();