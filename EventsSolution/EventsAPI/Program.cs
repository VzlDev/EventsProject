using EventsAPI.Data;
using EventsAPI.Data.Interfaces;
using EventsAPI.Health;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using HealthChecks.UI;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// Register MongoDB Connection
builder.Services.AddScoped<IMongoDbConnection, MongoDbConnection>();

var a = builder.Configuration.GetSection("MyDb:ConnectionString").Value;

// Register Health Checks
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetSection("MyDb:ConnectionString").Value)
    .AddCheck<EventsHealthCheck>("Events API Health Check");

// Register Health Checks UI
builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage(); // Add in-memory storage for health check results

builder.Services.AddScoped<IEventsRepository, EventsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Register Health Checks UI Middleware
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui"; // Change the UI path as desired
    options.ApiPath = "/healthchecks-api"; // Change the API path as desired
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
