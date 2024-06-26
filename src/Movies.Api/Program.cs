// Program.cs
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Behaviors;
using Movies.Api.Data;
using Movies.Api.Endpoints;
using Movies.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR and FluentValidation 
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Register global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

// Map the default endpoint
app.MapGet("/", () => "Welcome to the Movies API! \r\n\r\nIt's a pet project to learn minimal-api and Verstical Slice Architecture (VSA).");

// Map movie endpoints
app.MapMovieEndpoints();

await app.RunAsync();