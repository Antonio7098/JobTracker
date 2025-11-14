using JobTracker.Api.Models;
using JobTracker.Api.Services;
using JobTracker.Api.Endpoints;
using JobTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<IEmployersRepository, MySqlEmployersRepository>();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

builder.Services.AddDbContext<JobTrackerDbContext>(options =>
{

    options.UseMySql(connectionString, serverVersion);

}, ServiceLifetime.Scoped);

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var XmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, XmlFilename);

    options.IncludeXmlComments(xmlPath);
});

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

// Exception middleware
app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext context) => 
{
    var isDevelopment = app.Environment.IsDevelopment();
    return Results.Problem(
        title: "An error occurred",
        detail: isDevelopment ? "Check logs for details" : "An unexpected error occurred. Please try again later.",
        statusCode: StatusCodes.Status500InternalServerError
    );
});

app.MapEmployersEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();