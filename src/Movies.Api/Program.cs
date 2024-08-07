// Program.cs
using Carter;
using Common.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Behaviors;
using Movies.Api.Configuration;
using Movies.Api.Data;
using Movies.Api.Middleware;
using Movies.Api.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCarter();
builder.Services.AddProblemDetails();

// Register EF application db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Mapster
MapsterConfiguration.Configure();

// Register MediatR and FluentValidation 
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

// Register global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Register IDateTimeProvider
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

builder.Services.Configure<JsonOptions>(options =>
{
    //options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseStaticFiles();

// Map the default endpoint
app.MapGet("/", () => Results.Redirect("/Index.html"));

// Map movie endpoints
app.MapCarter();

await app.RunAsync();